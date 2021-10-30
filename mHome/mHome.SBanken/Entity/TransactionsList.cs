using System.Collections.Generic;
using JetBrains.Annotations;

namespace mHome.SBanken.Entity {
    public class TransactionsList {
        [UsedImplicitly]
        public int AvailableItems { get; set; }

        [UsedImplicitly]
        public List<Transaction> Items { get; set; } = null!;
    }
}