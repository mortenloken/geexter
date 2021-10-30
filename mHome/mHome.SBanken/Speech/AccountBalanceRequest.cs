using System.Text.RegularExpressions;
using mHome.Core.Speech.Input;
using mHome.Core.Speech.Language;

namespace mHome.SBanken.Speech {
    public class AccountBalanceRequest : ISpeechInput {
	    private const string AccountToken = "account";
        private static readonly Regex[] Expressions = {
            new($@"^what is the balance in my (?<{AccountToken}>[\w\s]+)$"),
            new($@"^what's the balance in my (?<{AccountToken}>[\w\s]+)$")
        };

        public string? Account { get; private init; }
        
        #region ISpeechInput implementation
        public ISpeechInput? Match(string input, Language language) {
            foreach (var expr in Expressions) {
                var match = expr.Match(input);
                if (!match.Success) continue;

                return new AccountBalanceRequest {
                    Account = match.Groups[AccountToken].Value
                };
            }
            return null;
        }
        #endregion
    }
}