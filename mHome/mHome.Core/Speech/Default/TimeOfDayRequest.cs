using System.Text.RegularExpressions;
using JetBrains.Annotations;
using mHome.Core.Speech.Input;

namespace mHome.Core.Speech.Default {
	[UsedImplicitly]
    public class TimeOfDayRequest : ISpeechInput {
        private static readonly Regex Expression = new(@"^what time is it$");
        
        #region ISpeechInput implementation
        public ISpeechInput? Match(string input, Core.Speech.Language.Language language) {
            return Expression.IsMatch(input)
                ? this
                : null;
        }
        #endregion
    }
}