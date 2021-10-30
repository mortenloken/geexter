using JetBrains.Annotations;

namespace mHome.SBanken.Entity {
    public class Account {
        [UsedImplicitly]
        public string AccountId { get; set; } = null!;

        [UsedImplicitly]
        public string Name { get; set; } = null!;
        
        [UsedImplicitly]
        public string AccountType { get; set; } = null!;
        
        [UsedImplicitly]
        public decimal Available { get; set; }
        
        [UsedImplicitly]
        public decimal Balance { get; set; }
    }
}