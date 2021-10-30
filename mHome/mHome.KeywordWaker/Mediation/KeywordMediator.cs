using System;
using System.Threading.Tasks;
using mHome.Core.Lights;
using mHome.Core.Mediation;
using mHome.Core.Speech.Bot;
using mHome.Core.Speech.Default;
using mHome.KeywordWaker.Options;

namespace mHome.KeywordWaker.Mediation {
    public interface IKeywordMediator : IMediator<Keyword> {}
    
    public class KeywordMediator : IKeywordMediator {
        private readonly ISpeechBot _bot;
        private readonly ILightsManager _lightsManager;

        #region Constructor methods
        public KeywordMediator(ISpeechBot bot, ILightsManager lightsManager) {
            _bot = bot;
            _lightsManager = lightsManager;
        }
        #endregion
        
        #region IMediator implementation
        public async Task MediateAsync(Keyword keyword) {
            switch (keyword.Meaning) {
                case KeywordMeaning.AcceptConversation:
                    await _bot.SpeakAsync(new AcceptConversation());
                    break;
                case KeywordMeaning.LightsOn:
                    await _lightsManager.SetScheduledScene();
                    break;
                case KeywordMeaning.LightsOff:
                    await _lightsManager.LightsOnOffAsync(false);
                    break;
                case null:
	                throw new ArgumentOutOfRangeException(nameof(keyword));
                default:
	                throw new ArgumentOutOfRangeException(nameof(keyword));
            }
        }
        #endregion
    }
}