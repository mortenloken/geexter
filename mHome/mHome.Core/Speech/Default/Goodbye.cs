using System.Threading.Tasks;
using JetBrains.Annotations;
using mHome.Core.DateAndTime;
using mHome.Core.Speech.Output;

namespace mHome.Core.Speech.Default {
	[PublicAPI]
    public class Goodbye : ISpeechOutput {
        private readonly TimeOfDay _timeOfDay;

        #region Constructor methods
        public Goodbye(TimeOfDay timeOfDay) {
            _timeOfDay = timeOfDay;
        }
        public Goodbye() : this(DateAndTimeUtil.TimeOfDay) {}
        #endregion
        
        #region ISpeechOutput implementationm
        public Task<string> ResolveAsync() {
	        return _timeOfDay switch {
		        TimeOfDay.Night => Task.FromResult("Good night, sleep tight."),
		        TimeOfDay.Morning => Task.FromResult("Goodbye, have a great day"),
		        TimeOfDay.Midday => Task.FromResult("Goodbye, have a great lunch."),
		        TimeOfDay.Afternoon => Task.FromResult("Goodbye, have a great dinner."),
		        TimeOfDay.Evening => Task.FromResult("Good night, have a great evening."),
		        _ => Task.FromResult(string.Empty)
	        };
        }
        public bool ExpectResponse => false;
        #endregion
    }
}