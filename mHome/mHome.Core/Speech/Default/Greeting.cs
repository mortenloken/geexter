using System.Threading.Tasks;
using JetBrains.Annotations;
using mHome.Core.DateAndTime;
using mHome.Core.Speech.Output;

namespace mHome.Core.Speech.Default {
	[PublicAPI]
    public class Greeting : ISpeechOutput {
        private readonly TimeOfDay _timeOfDay;

        #region Constructor methods
        public Greeting(TimeOfDay timeOfDay) {
            _timeOfDay = timeOfDay;
        }
        public Greeting() : this(DateAndTimeUtil.TimeOfDay) {}
        #endregion
        
        #region ISpeechOutput implementationm
        public Task<string> ResolveAsync() {
	        return _timeOfDay switch {
		        TimeOfDay.Night => Task.FromResult("Greetings night hawk."),
		        TimeOfDay.Morning => Task.FromResult("Good morning master."),
		        TimeOfDay.Midday => Task.FromResult("Good day sir."),
		        TimeOfDay.Afternoon => Task.FromResult("Good afternoon good sir."),
		        TimeOfDay.Evening => Task.FromResult("Good afternoon master."),
		        _ => Task.FromResult(string.Empty)
	        };
        }
        
        public bool ExpectResponse => false;
        #endregion
    }
}