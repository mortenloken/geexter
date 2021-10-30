using System.Threading.Tasks;
using JetBrains.Annotations;
using mHome.Core.Speech.Output;

namespace mHome.Core.Speech.Default {
	[PublicAPI]
    public class SystemInitialized : ISpeechOutput {
        #region ISpeechOutput implementationm
        public Task<string> ResolveAsync() {
            return Task.FromResult("System initialization completed.");
        }
        
        public bool ExpectResponse => false;
        #endregion        
    }
}