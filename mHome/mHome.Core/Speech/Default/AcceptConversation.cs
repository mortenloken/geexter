using System.Threading.Tasks;
using mHome.Core.Extensions;
using mHome.Core.Speech.Output;

namespace mHome.Core.Speech.Default {
    public class AcceptConversation : ISpeechOutput {
        #region ISpeechOutput implementation
        public Task<string> ResolveAsync() {
            return Task.FromResult(new[] {
                "Yes, master ?.",
                "Yes, my lord ?.",
                "Hello, what can I do for you ?."
            }.Random());
        }
        public bool ExpectResponse => true;
        #endregion
    }
}