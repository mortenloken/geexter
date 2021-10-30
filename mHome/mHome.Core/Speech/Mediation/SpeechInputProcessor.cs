using System.Collections.Generic;
using System.Linq;
using mHome.Core.Reflection;
using mHome.Core.Speech.Input;
using mHome.Core.Voice;
using Microsoft.Extensions.Logging;

namespace mHome.Core.Speech.Mediation {
    public interface ISpeechInputProcessor {
        ISpeechInput? Resolve(string input);
    }

    public class SpeechInputProcessor : ISpeechInputProcessor {
        private readonly List<ITypeResolver<ISpeechInput>> _speechInputResolvers;
        private readonly IVoiceService _voiceService;
        private readonly ILogger<SpeechInputProcessor> _logger;

        #region Constructor methods
        public SpeechInputProcessor(IEnumerable<ITypeResolver<ISpeechInput>> speechInputResolvers, IVoiceService voiceService, ILogger<SpeechInputProcessor> logger) {
            _speechInputResolvers = speechInputResolvers.ToList();
            _voiceService = voiceService;
            _logger = logger;
        }
        #endregion
        
        #region ISpeechInputProcessor implementation
        public ISpeechInput? Resolve(string input) {
            input = NormalizeInput(input);
            if (string.IsNullOrEmpty(input)) {
                _logger.LogInformation("Empty input...");
                return null;
            }
            
            //resolve the language in use
            var language = _voiceService.Voice.Language;

            //find the first input which matches
            foreach (var sir in _speechInputResolvers) {
                foreach (var type in sir.GetKnownInstances()) {
                    var match = type.Match(input, language);
                    if (match != null) return match;
                }
            }

            return null;
        }
        #endregion
        
        #region Private methods
        private static string NormalizeInput(string? s) {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            s = s.ToLower().Trim();
            if (s.EndsWith(".")) s = s.Substring(0, s.Length - 1);
            if (s.EndsWith("?")) s = s.Substring(0, s.Length - 1);
            return s.Trim();
        }
        #endregion
    }
}