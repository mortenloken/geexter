using JetBrains.Annotations;

namespace mHome.KeywordWaker.Options {
    public class KeywordWakerOptions {
        public const string SectionName = "KeywordWaker";
        
        [UsedImplicitly]
        public Keyword[] Keywords { get; set; } = null!;
    }
    
    public class Keyword {
        [UsedImplicitly]
        public KeywordMeaning? Meaning { get; set; }

        [UsedImplicitly]
        public string Phrase { get; set; } = null!;
        
        [UsedImplicitly]
        public string File { get; set; } = null!;
    }

    public enum KeywordMeaning {
        AcceptConversation,
        LightsOn,
        LightsOff
    }
}