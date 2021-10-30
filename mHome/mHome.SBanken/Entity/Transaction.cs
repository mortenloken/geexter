using JetBrains.Annotations;

namespace mHome.SBanken.Entity {
    public class Transaction {
        [UsedImplicitly]
        public decimal Amount { get; set; }

        [UsedImplicitly]
        public string TransactionType { get; set; } = null!;
        
        [UsedImplicitly]
        public string Text { get; set; } = null!;
    }
}