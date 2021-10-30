using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mHome.Core.Extensions;
using mHome.Core.Speech.Input;
using mHome.Core.Speech.Mediation;
using mHome.Core.Speech.Output;

namespace mHome.Waste.Speech {
    public class WasteSpeechMediator : IRequestResponseMediator {
        private readonly IServiceProvider _serviceProvider;

        #region Consstructor methods
        public WasteSpeechMediator(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }
        #endregion
        
        #region IRequestResponseMediator implementation
        public Task<IEnumerable<IOutput>?> MediateAsync(ISpeechInput request) {
	        return request switch {
		        NextWasteRequest => Task.FromResult<IEnumerable<IOutput>?>(new NextWasteResponse(_serviceProvider).Arrayify()),
		        NextSpecificWasteRequest nextSpecificWasteRequest => Task.FromResult<IEnumerable<IOutput>?>(new NextSpecificWasteResponse(_serviceProvider, nextSpecificWasteRequest.Waste!).Arrayify()),
		        _ => Task.FromResult<IEnumerable<IOutput>?>(null)
	        };
        }
        #endregion
    }
}