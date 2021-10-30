using System;

namespace mHome.Core.DateAndTime {
    public static class DateAndTimeUtil {
        public static TimeOfDay TimeOfDay {
            get {
                var hour = DateTime.Now.Hour;
                return hour switch {
	                < 6 => TimeOfDay.Night,
	                < 10 => TimeOfDay.Morning,
	                < 16 => TimeOfDay.Midday,
	                < 20 => TimeOfDay.Afternoon,
	                _ => TimeOfDay.Evening
                };
            }
        }
    }
}