using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mHome.Core.Extensions;
using mHome.Core.Speech.Input;
using mHome.Core.Speech.Mediation;
using mHome.Core.Speech.Output;

namespace mHome.SBanken.Speech {
    public class SBankenSpeechMediator : IRequestResponseMediator {
        private readonly IServiceProvider _serviceProvider;

        #region Consstructor methods
        public SBankenSpeechMediator(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }
        #endregion
        
        #region IRequestResponseMediator implementation
        public Task<IEnumerable<IOutput>?> MediateAsync(ISpeechInput request) {
            if (request is AccountBalanceRequest accountBalanceRequest) {
                return Task.FromResult<IEnumerable<IOutput>?>(new AccountBalanceResponse(_serviceProvider, accountBalanceRequest.Account!).Arrayify());
            }
            return Task.FromResult<IEnumerable<IOutput>?>(null);
        }
        #endregion
    }
}