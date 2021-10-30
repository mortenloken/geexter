using System.Text.RegularExpressions;
using mHome.Core.Speech.Input;
using mHome.Core.Speech.Language;

namespace mHome.Waste.Speech {
    public class NextSpecificWasteRequest : ISpeechInput {
	    private const string WasteToken = "waste";
        private static readonly Regex Expression = new($@"^when is the next (?<{WasteToken}>[\w\s]+) waste$");

        public string? Waste { get; private init; }
        
        #region ISpeechInput implementation
        public ISpeechInput? Match(string input, Language language) {
            var match = Expression.Match(input);
            if (!match.Success) return null;

            return new NextSpecificWasteRequest {
                Waste = match.Groups[WasteToken].Value
            };
        }
        #endregion
    }
}