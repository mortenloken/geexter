using System;
using System.Collections.Generic;

namespace mHome.Waste.Entity {
    public record WasteDate {
        public DateTime Date { get; }
        public List<WasteType> Types { get; }

        public WasteDate(DateTime date) {
            Date = date;
            Types = new List<WasteType>();
        }
    }
}