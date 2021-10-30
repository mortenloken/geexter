using System.Threading.Tasks;
using mHome.Core.Speech.Output;

namespace mHome.Core.Speech.Default {
    public class EndConversation : ISpeechOutput {
        #region ISpeechOutput implementationm
        public Task<string> ResolveAsync() {
            return Task.FromResult("Alright, I'll leave you to it.");
        }
        public bool ExpectResponse => false;
        #endregion
    }
}