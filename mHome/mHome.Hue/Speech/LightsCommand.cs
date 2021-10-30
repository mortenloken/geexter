using System.Text.RegularExpressions;
using mHome.Core.Speech.Input;
using mHome.Core.Speech.Language;

namespace mHome.Hue.Speech {
    public class LightsCommand : ISpeechInput {
	    private const string LightsToken = "lights";
        private static readonly Regex[] Expressions = {
            new($@"^set lights (?<{LightsToken}>[\w\s]+)$"),
            new($@"^satellites (?<{LightsToken}>[\w\s]+)$")
        };

        public string? Lights { get; private init; }
        
        #region ISpeechInput implementation
        public ISpeechInput? Match(string input, Language language) {
            foreach (var expr in Expressions) {
                var match = expr.Match(input);
                if (!match.Success) continue;

                return new LightsCommand {
                    Lights = match.Groups[LightsToken].Value
                };
            }

            return null;
        }
        #endregion
    }
}