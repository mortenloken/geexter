using System;
using System.Threading.Tasks;
using mHome.Core.Mediation;
using mHome.Core.Speech.Bot;
using mHome.Core.Speech.Default;
using Microsoft.Extensions.DependencyInjection;

namespace mHome.Hue.Mediation {
    public interface IHueMediator : IMediator<HueEvent> {}
    
    public class HueMediator : IHueMediator {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISpeechBot _bot;

        #region Constructor methods
        public HueMediator(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
            _bot = serviceProvider.GetRequiredService<ISpeechBot>();
        }
        #endregion
        
        #region IMediator implementation
        public async Task MediateAsync(HueEvent @event) {
	        switch (@event) {
		        case HueEvent.LightsOn:
			        await _bot.SpeakAsync(
				        new Greeting(),
				        new IntroduceVoice(_serviceProvider),
				        new TimeOfDayResponse()
			        );
			        break;
		        case HueEvent.LightsOff:
			        await _bot.SpeakAsync(new Goodbye());
			        break;
		        default:
			        throw new ArgumentOutOfRangeException(nameof(@event), @event, null);
	        }
        }
        #endregion
    }
}