using System;
using System.Threading.Tasks;
using mHome.Core.Speech.Output;

namespace mHome.Core.Speech.Bot {
    public class NullSpeechBot : ISpeechBot {
        #region ISpeechBot implementation
        public Task SpeakAsync(string text) => Task.CompletedTask;
        public Task SpeakAsync(params ISpeechOutput[] output) => Task.CompletedTask;
        public Task SpeakAsync(ISpeechOutput output) => Task.CompletedTask;

        public void Dispose() {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}