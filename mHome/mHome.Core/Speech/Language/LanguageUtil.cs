using System.Globalization;

namespace mHome.Core.Speech.Language {
    public static class LanguageUtil {
        public static CultureInfo GetCulture(Language language) {
	        return language switch {
		        Language.English => new CultureInfo("en-US"),
		        Language.Norwegian => new CultureInfo("nb-NO"),
		        _ => CultureInfo.InvariantCulture
	        };
        }
    }
}