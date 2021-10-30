using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mHome.Core.Extensions;
using mHome.Core.Speech.Input;
using mHome.Core.Speech.Mediation;
using mHome.Core.Speech.Output;

namespace mHome.Hue.Speech {
    public class HueSpeechMediator : IRequestResponseMediator {
        private readonly IServiceProvider _serviceProvider;

        #region Consstructor methods
        public HueSpeechMediator(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }
        #endregion
        
        #region IRequestResponseMediator implementation
        public Task<IEnumerable<IOutput>?> MediateAsync(ISpeechInput request) {
            if (request is LightsCommand lightsCommand) {
                return Task.FromResult<IEnumerable<IOutput>?>(new LightsCommandOutput(_serviceProvider, lightsCommand.Lights!).Arrayify());
            }
            return Task.FromResult<IEnumerable<IOutput>?>(null);
        }
        #endregion
    }
}