using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mHome.Bot.Options;
using mHome.Core.Speech.Bot;
using mHome.Core.Speech.Default;
using mHome.Core.Speech.Mediation;
using mHome.Core.Speech.Output;
using mHome.Core.Voice;
using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace mHome.Bot {
    public class SpeechBot : ISpeechBot {
        private readonly ISpeechInputProcessor _speechInputProcessor;
        private readonly List<IRequestResponseMediator> _mediators;
        private readonly ILogger<SpeechBot> _logger;

        private readonly SpeechSynthesizer? _synthesizer;
        private readonly SpeechRecognizer? _recognizer;
        
        #region Constructor methods
        public SpeechBot(
            IVoiceService voiceService,
            ISpeechInputProcessor speechInputProcessor,
            IEnumerable<IRequestResponseMediator> mediators,
            IOptions<BotOptions> botOptions, 
            ILogger<SpeechBot> logger) {
            _speechInputProcessor = speechInputProcessor;
            _mediators = mediators.ToList();
            _logger = logger;

            _logger.LogInformation("Initializing bot");

            //create speech config
            var speechConfig = SpeechConfig.FromSubscription(botOptions.Value.SubscriptionKey, botOptions.Value.Region);
            speechConfig.SpeechSynthesisVoiceName = voiceService.Voice.VoiceName;
            speechConfig.SetProfanity(ProfanityOption.Raw);

            //create speech synthesizer
            _synthesizer = new SpeechSynthesizer(speechConfig);
            
            //create speech recognizer
            _recognizer = new SpeechRecognizer(speechConfig);
        }
        #endregion

        #region IBot implementation
        public async Task SpeakAsync(string text) {
            if(_synthesizer == null) throw new ApplicationException("Bot not initialized.");
            
            //speak raw text
            _logger.LogInformation($"Saying: '{text}'.");
            await _synthesizer.SpeakTextAsync(text);
        }

        public Task SpeakAsync(params ISpeechOutput[] output) => SpeakAsync(new CompositeSpeechUtterance(output));

        public async Task SpeakAsync(ISpeechOutput output) {
            //speak the text
            var speak = await output.ResolveAsync();
            await SpeakAsync(speak!);
            
            //listen for request ?
            if (output.ExpectResponse) await RequestResponseAsync();
        }
        #endregion

        #region Private methods
        private async Task RequestResponseAsync() {
            //listen for a request
            var request = await ListenAsync();

            //resolve the request
            var resolvedRequest = _speechInputProcessor.Resolve(request);
            if (resolvedRequest == null) { //not resolved
                await SpeakAsync(new NoUnderstand());
                return;
            }

            //let all mediators mediate and produce command or speech output
            foreach (var mediator in _mediators) {
                var outputs = (await mediator.MediateAsync(resolvedRequest))?.ToList() ?? new List<IOutput>();
                foreach (var output in outputs) {
                    switch (output) {
                        case ISpeechOutput speechOutput:
                            await SpeakAsync(speechOutput);
                            break;
                        case ICommandOutput commandOutput:
                            await commandOutput.InvokeAsync();
                            break;
                    }
                }
            }
        }
        
        private async Task<string> ListenAsync() {
            if(_recognizer == null) throw new ApplicationException("Bot not initialized.");

            _logger.LogInformation("Listening...");
            var result = await _recognizer.RecognizeOnceAsync();
            _logger.LogInformation("Recognized '{text}' with as duration of {duration} ms and reason '{reason}'.", result.Text, (int)result.Duration.TotalMilliseconds, result.Reason);
            return result.Text;
        }
        #endregion
        
        #region IDisposable implementation
        public void Dispose() {
            _synthesizer?.Dispose();
            _recognizer?.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}