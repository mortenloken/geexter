using System.Threading.Tasks;
using mHome.Core.Speech.Output;

namespace mHome.Core.Speech.Default {
    public class NoUnderstand : ISpeechOutput {
        #region ISpeechOutput implementationm
        public Task<string> ResolveAsync() {
            return Task.FromResult("I cannot understand that. Fuck off.");
        }
        
        public bool ExpectResponse => false;
        #endregion
    }
}