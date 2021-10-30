using JetBrains.Annotations;

namespace mHome.SBanken.Options {
    public class SBankenOptions {
        public const string SectionName = "SBanken";
        
        [UsedImplicitly]
        public string CustomerId { get; set; } = null!;

        [UsedImplicitly]
        public string ClientId { get; set; } = null!;
        
        [UsedImplicitly]
        public string ClientSecret { get; set; } = null!;
        
        [UsedImplicitly]
        public Account[] Accounts { get; set; } = null!;
    }
    
    public class Account {
        [UsedImplicitly]
        public string Id { get; set; } = null!;

        [UsedImplicitly]
        public string NickName { get; set; } = null!;
    }
}