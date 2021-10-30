using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mHome.Core.Extensions;
using mHome.Core.Speech.Default;
using mHome.Core.Speech.Input;
using mHome.Core.Speech.Output;

namespace mHome.Core.Speech.Mediation {
    public class DefaultRequestResponseMediator : IRequestResponseMediator {
        #region IRequestResponseMediator implementation
        public Task<IEnumerable<IOutput>?> MediateAsync(ISpeechInput request) {
	        return request switch {
		        LoveYou => Task.FromResult<IEnumerable<IOutput>?>(new LoveYouToo().Arrayify()),
		        FuckYou => Task.FromResult<IEnumerable<IOutput>?>(new FuckYouToo().Arrayify()),
		        TimeOfDayRequest => Task.FromResult<IEnumerable<IOutput>?>(new TimeOfDayResponse().Arrayify()),
		        EndConversationRequest => Task.FromResult<IEnumerable<IOutput>?>(new EndConversation().Arrayify()),
		        _ => Task.FromResult<IEnumerable<IOutput>?>(Enumerable.Empty<IOutput>())
	        };
        }
        #endregion
    }
}