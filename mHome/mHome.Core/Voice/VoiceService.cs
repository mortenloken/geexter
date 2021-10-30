using JetBrains.Annotations;
using mHome.Core.Extensions;

namespace mHome.Core.Voice {
	[PublicAPI]
    public interface IVoiceService {
        IVoice Voice { get; }
    }
    
    [PublicAPI]
    public class VoiceService : IVoiceService {
        #region Constructor methods
        public VoiceService() {
            Voice = Core.Voice.Voice.All.Random()!;
        }
        public VoiceService(IVoice initialVoice) {
            Voice = initialVoice;
        }
        #endregion
        
        #region IVoiceService implementation
        public IVoice Voice { get; }
        #endregion
    }
}