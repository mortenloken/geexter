using System;
using System.Threading.Tasks;
using mHome.Core.Speech.Output;
using mHome.Core.Voice;
using Microsoft.Extensions.DependencyInjection;

namespace mHome.Core.Speech.Default {
    public class IntroduceVoice : ISpeechOutput {
        private readonly IServiceProvider _serviceProvider;

        #region Constructor methods
        public IntroduceVoice(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }
        #endregion
        
        #region ISpeechOutput implementation
        public Task<string> ResolveAsync() {
            var voice = _serviceProvider.GetRequiredService<IVoiceService>().Voice;
            return Task.FromResult($"My name is {voice.Name}.");
        }
        
        public bool ExpectResponse => false;
        #endregion
    }
}