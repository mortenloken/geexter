using JetBrains.Annotations;

namespace mHome.Bot.Options {
    public class BotOptions {
        public const string SectionName = "Bot";
        
        [UsedImplicitly]
        public string SubscriptionKey { get; set; } = null!;

        [UsedImplicitly]
        public string Region { get; set; } = null!;
    }
}