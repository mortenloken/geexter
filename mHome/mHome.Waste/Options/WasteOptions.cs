using JetBrains.Annotations;

namespace mHome.Waste.Options {
    public class WasteOptions {
        public const string SectionName = "Waste";
        
        [UsedImplicitly]
        public string ServiceUrl { get; set; } = null!;
    }
}