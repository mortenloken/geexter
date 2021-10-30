using System.Threading.Tasks;
using mHome.Core.Speech.Output;

namespace mHome.Core.Speech.Default {
    public class LoveYouToo : ISpeechOutput {
        #region ISpeechOutput implementationm
        public Task<string> ResolveAsync() {
            return Task.FromResult("I love you too master. You are truly awesome.");
        }
        
        public bool ExpectResponse => false;
        #endregion
    }
}