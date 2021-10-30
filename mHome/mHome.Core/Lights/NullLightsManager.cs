using System;
using System.Threading.Tasks;

namespace mHome.Core.Lights {
    public class NullLightsManager : ILightsManager {
        #region ILightsManager implementation
        public Task LightsOnOffAsync(bool on) => Task.CompletedTask;
        public Task LightsOnOffAsync(string groupName, bool on) => Task.CompletedTask;
        public Task<string[]> GetScenesAsync() => Task.FromResult(Array.Empty<string>());
        public Task SetSceneAsync(string sceneName) => Task.CompletedTask;
        public Task SetSceneAsync(string groupName, string sceneName) => Task.CompletedTask;
        public Task SetScheduledScene() => Task.CompletedTask;
        #endregion
    }
}