using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using JetBrains.Annotations;
using mHome.SBanken.Entity;
using mHome.SBanken.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Account = mHome.SBanken.Entity.Account;

namespace mHome.SBanken {
	[PublicAPI]
    public interface ISBankenClient {
        Task<AccountsList> GetAccountsAsync();
        Task<Account?> GetAccountByNickNameAsync(string nickName);
        Task<Account?> GetAccountByIdAsync(string id);
    }
    
    [PublicAPI]
    public class SBankenClient : ISBankenClient {
        //private const string DiscoveryEndpoint = "https://auth.sbanken.no/identityserver";
        private const string TokenEndpoint = "https://auth.sbanken.no/identityserver/connect/token";
        private const string ApiBaseAddress = "https://api.sbanken.no";
        private const string BankBasePath = "/exec.bank";
        //private const string CustomersBasePath = "/exec.customers";

        private readonly SBankenOptions _options;

        #region Constructor methods
        public SBankenClient(IOptions<SBankenOptions> options) {
            _options = options.Value;
        }
        #endregion
        
        #region ISBankenClient implementation
        public async Task<AccountsList> GetAccountsAsync() {
            var client = await CreateClient();
            var response = await client.GetAsync($"{BankBasePath}/api/v1/Accounts");
            var raw = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AccountsList>(raw);
        }
        
        public async Task<Account?> GetAccountByNickNameAsync(string nickName) {
            var account = _options.Accounts.SingleOrDefault(a => string.Equals(a.NickName, nickName, StringComparison.CurrentCultureIgnoreCase));
            return account != null
                ? await GetAccountByIdAsync(account.Id)
                : null;
        }

        public async Task<Account?> GetAccountByIdAsync(string id) {
            var client = await CreateClient();
            var response = await client.GetAsync($"{BankBasePath}/api/v1/Accounts/{id}");
            var raw = await response.Content.ReadAsStringAsync();
            return JObject.Parse(raw)
                .GetValue("item")?
                .ToObject(typeof(Account)) as Account;
        }
        #endregion
        
        #region Private methods
        private async Task<HttpClient> CreateClient() {
            //get an access token
            var tokenResponse = await new HttpClient().RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest {
                Address = TokenEndpoint,
                ClientId = _options.ClientId,
                ClientSecret = _options.ClientSecret
            });
            var accessToken = tokenResponse.AccessToken;

            //create the client
            var client = new HttpClient {
                BaseAddress = new Uri(ApiBaseAddress),
                DefaultRequestHeaders = {
                    {"customerId", _options.CustomerId}
                }
            };
            client.SetBearerToken(accessToken);

            return client;
        }
        #endregion
    }
}