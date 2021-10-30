using JetBrains.Annotations;

namespace mHome.Core.Voice {
    [PublicAPI]
    public interface IVoice {
        string Name { get; }
        string VoiceName { get; }
        Speech.Language.Language Language { get; }
    }
    
    [PublicAPI]
    public class Voice : IVoice {
        #region IVoice implementation
        public string Name { get; }
        public string VoiceName { get; }
        public Speech.Language.Language Language { get; }
        #endregion

        #region Instances
        public static readonly Voice Iselin = new("Solveig", "nb-NO-IselinNeural", Speech.Language.Language.Norwegian);
        public static readonly Voice Aria = new("Aria", "en-US-AriaNeural", Speech.Language.Language.English);
        public static readonly Voice Ryan = new("Ryan", "en-GB-RyanNeural", Speech.Language.Language.English);
        public static readonly Voice Mia = new("Mia", "en-GB-MiaNeural", Speech.Language.Language.English);
        public static readonly Voice Libby = new("Libby", "en-GB-LibbyNeural", Speech.Language.Language.English);
        public static readonly Voice Emily = new("Emily", "en-IE-EmilyNeural", Speech.Language.Language.English);
        public static readonly Voice[] All = { Aria, Ryan, Mia, Libby, Emily };
        #endregion
        
        #region Constructor methods
        private Voice(string name, string voiceName, Speech.Language.Language language) {
            Name = name;
            VoiceName = voiceName;
            Language = language;
        }
        #endregion
    }
}