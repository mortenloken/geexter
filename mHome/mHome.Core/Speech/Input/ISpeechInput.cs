using JetBrains.Annotations;

namespace mHome.Core.Speech.Input {
	[PublicAPI]
    public interface ISpeechInput {
        ISpeechInput? Match(string input, Language.Language language);
    }
}