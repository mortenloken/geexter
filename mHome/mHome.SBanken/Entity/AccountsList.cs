using System.Collections.Generic;
using JetBrains.Annotations;

namespace mHome.SBanken.Entity {
    public class AccountsList {
        [UsedImplicitly]
        public int AvailableItems { get; set; }

        [UsedImplicitly]
        public List<Account> Items { get; set; } = null!;
    }
}