using JetBrains.Annotations;

namespace mHome.Hue.Options {
    public class HueOptions {
        public const string SectionName = "Hue";
        
        [UsedImplicitly]
        public string AppKey { get; set; } = null!;

        [UsedImplicitly]
        public Room Room { get; set; } = null!;
    }

    public class Room {
        [UsedImplicitly]
        public string Name { get; set; } = null!;

        [UsedImplicitly]
        public SceneScheduleEntry[] SceneSchedule { get; set; } = null!;
    }

    public class SceneScheduleEntry {
        [UsedImplicitly]
        public int From { get; set; }

        [UsedImplicitly]
        public int To { get; set; }
        
        [UsedImplicitly]
        public string Scene { get; set; } = null!;
    }
}