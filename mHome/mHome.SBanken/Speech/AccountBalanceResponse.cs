using System;
using System.Threading.Tasks;
using mHome.Core.Speech.Output;
using Microsoft.Extensions.DependencyInjection;
using Nut;

namespace mHome.SBanken.Speech {
    public class AccountBalanceResponse : ISpeechOutput {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _account;
        
        #region Constructor methods
        public AccountBalanceResponse(IServiceProvider serviceProvider, string account) {
            _serviceProvider = serviceProvider;
            _account = account;
        }
        #endregion
        
        #region ISpeechOutput implementation
        public async Task<string> ResolveAsync() {
            var client = _serviceProvider.GetRequiredService<ISBankenClient>();
            var account = await client.GetAccountByNickNameAsync(_account);

            return account != null
                ? $"You have {((int) account.Available).ToText()} kroners in your {_account}."
                : "That account is not known.";
        }
        
        public bool ExpectResponse => false;
        #endregion
    }
}