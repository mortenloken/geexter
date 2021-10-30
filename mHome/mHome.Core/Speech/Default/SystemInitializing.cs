using System.Threading.Tasks;
using JetBrains.Annotations;
using mHome.Core.Speech.Output;

namespace mHome.Core.Speech.Default {
	[PublicAPI]
    public class SystemInitializing : ISpeechOutput {
        #region ISpeechOutput implementationm
        public Task<string> ResolveAsync() {
            return Task.FromResult("System initializing.");
        }
        
        public bool ExpectResponse => false;
        #endregion        
    }
}