using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using mHome.Core.Speech.Input;

namespace mHome.Core.Speech.Default {
	[UsedImplicitly]
    public class EndConversationRequest : ISpeechInput {
        private static readonly Regex[] Expressions = {
            new(@"^stop$"),
            new(@"^nothing$")
        };
        
        #region ISpeechInput implementation
        public ISpeechInput? Match(string input, Core.Speech.Language.Language language) {
            return Expressions.Any(e => e.IsMatch(input))
                ? this
                : null;
        }
        #endregion
    }
}