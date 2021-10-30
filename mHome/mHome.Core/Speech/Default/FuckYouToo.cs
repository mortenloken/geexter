using System.Threading.Tasks;
using mHome.Core.Speech.Output;

namespace mHome.Core.Speech.Default {
    public class FuckYouToo : ISpeechOutput {
        #region ISpeechOutput implementationm
        public Task<string> ResolveAsync() {
            return Task.FromResult("That's rude! Fuck you too.");
        }
        public bool ExpectResponse => false;
        #endregion
    }
}