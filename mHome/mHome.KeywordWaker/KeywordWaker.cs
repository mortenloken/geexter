using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using mHome.KeywordWaker.Mediation;
using mHome.KeywordWaker.Options;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace mHome.KeywordWaker {
    public class KeywordWaker : IHostedService, IDisposable {
        private readonly KeywordWakerOptions _options;
        private readonly IKeywordMediator _mediator;
        private readonly IHostEnvironment _environment;
        private readonly ILogger<KeywordWaker> _logger;
        private readonly CancellationTokenSource _cts;

        #region Constructor methods
        public KeywordWaker(
            IKeywordMediator mediator,
            IHostEnvironment environment,
            IOptions<KeywordWakerOptions> options, 
            ILogger<KeywordWaker> logger) {
            _mediator = mediator;
            _environment = environment;
            _options = options.Value;
            _logger = logger;
            _cts = new CancellationTokenSource();
        }
        #endregion
        
        #region IHostedService implementation
        public Task StartAsync(CancellationToken cancellationToken) {
            _logger.LogInformation("Initializing keyword waker...");
            foreach (var keyword in _options.Keywords) {
                //this task never completes, so don't await it
                _ = WakeAsync(keyword, _cts.Token);
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            //cancel the service's cancellation token
            _cts.Cancel();
            return Task.CompletedTask;
        }
        #endregion

        #region IDisposable implementation
        public void Dispose() {
	        _cts.Dispose();
	        GC.SuppressFinalize(this);
        }
        #endregion
        
        #region Private methods
        private Task WakeAsync(Keyword keyword, CancellationToken ct) {
            var keywordFile = Path.Combine(_environment.ContentRootPath, keyword.File);
            _logger.LogDebug("Initializing keyword recognition with wake file '{file}'.", keywordFile);

            var keywordModel = KeywordRecognitionModel.FromFile(keywordFile);
            var keywordRecognizerConfig = AudioConfig.FromDefaultMicrophoneInput();
            var keywordRecognizer = new KeywordRecognizer(keywordRecognizerConfig);
            
            return Task.Factory.StartNew(async () => {
                while (true) {
                    if (ct.IsCancellationRequested) break;
                    var res = await keywordRecognizer.RecognizeOnceAsync(keywordModel);
                    var text = res.Text?.Trim();
                    if (!string.IsNullOrEmpty(text)) {
                        _logger.LogInformation("Recognized keyword '{keyword}' as '{text}'.", keyword.Phrase, text);
                        await _mediator.MediateAsync(keyword);
                    }
                    else throw new ApplicationException("Unknown keyword recognition");
                }
            }, ct);
        }
        #endregion
    }
}