using System.Text.RegularExpressions;
using JetBrains.Annotations;
using mHome.Core.Speech.Input;
using mHome.Core.Speech.Language;

namespace mHome.Waste.Speech {
	[UsedImplicitly]
    public class NextWasteRequest : ISpeechInput {
        private static readonly Regex Expression = new(@"^what is the next waste$");
        
        #region ISpeechInput implementation
        public ISpeechInput? Match(string input, Language language) {
            return Expression.IsMatch(input)
                ? this
                : null;
        }
        #endregion
    }
}