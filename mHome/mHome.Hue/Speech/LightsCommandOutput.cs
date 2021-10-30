using System;
using System.Linq;
using System.Threading.Tasks;
using mHome.Core.Lights;
using mHome.Core.Speech.Bot;
using mHome.Core.Speech.Output;
using Microsoft.Extensions.DependencyInjection;

namespace mHome.Hue.Speech {
    public class LightsCommandOutput : ICommandOutput {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _lightsToken;

        #region Constructor methods
        public LightsCommandOutput(IServiceProvider serviceProvider, string lightsToken) {
            _serviceProvider = serviceProvider;
            _lightsToken = lightsToken;
        }
        #endregion
        
        #region ICommandOutput implementation
        public async Task InvokeAsync() {
            var hueManager = _serviceProvider.GetRequiredService<ILightsManager>();
            var bot = _serviceProvider.GetRequiredService<ISpeechBot>();
            
            //check if the token matches a scene in the default room
            var scenes = await hueManager.GetScenesAsync();
            var scene = scenes.FirstOrDefault(s => s.ToLower() == _lightsToken);
            if (scene != null) {
                await hueManager.SetSceneAsync(scene);
                return;
            }

            await bot.SpeakAsync($"{_lightsToken} is not a known lights command.");
        }
        #endregion
    }
}