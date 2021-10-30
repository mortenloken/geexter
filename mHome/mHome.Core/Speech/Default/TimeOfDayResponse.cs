using System;
using System.Threading.Tasks;
using mHome.Core.Speech.Language;
using mHome.Core.Speech.Output;

namespace mHome.Core.Speech.Default {
    public class TimeOfDayResponse : ISpeechOutput {
        #region ISpeechOutput implementationm
        public Task<string> ResolveAsync() {
            return Task.FromResult($"It is {DateTime.Now.ToString("dddd dd MMMM, hh:mm tt", LanguageUtil.GetCulture(Core.Speech.Language.Language.English))}.");
        }
        
        public bool ExpectResponse => false;
        #endregion
    }
}