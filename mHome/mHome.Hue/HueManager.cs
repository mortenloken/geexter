using System;
using System.Linq;
using System.Threading.Tasks;
using mHome.Core.Lights;
using mHome.Hue.Options;
using mHome.Hue.Util;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Q42.HueApi;
using Q42.HueApi.Interfaces;
using Q42.HueApi.Models;
using Q42.HueApi.Models.Groups;

namespace mHome.Hue {
    public class HueManager : ILightsManager {
        private readonly ILogger<HueManager> _logger;
        private readonly HueOptions _options;
        private IHueClient? _client;

        #region Constructor methods
        public HueManager(IOptions<HueOptions> options, ILogger<HueManager> logger) {
            _logger = logger;
            _options = options.Value;
        }
        #endregion

        #region IHueManager implementation
        public Task LightsOnOffAsync(bool on) => LightsOnOffAsync(_options.Room.Name, on);

        public async Task LightsOnOffAsync(string groupName, bool on) {
            _client ??= await HueUtils.GetClient(_options.AppKey);

            //get the group
            var group = await GetGroup(groupName);

            //issue the light command
            var cmd = new LightCommand {On = on};
            await _client.SendCommandAsync(cmd, group.Lights);
        }

        public async Task<string[]> GetScenesAsync() {
            _client ??= await HueUtils.GetClient(_options.AppKey);

            //get the group
            var group = await GetGroup(_options.Room.Name);
            var groupId = group.Id;

            var scenes = await _client!.GetScenesAsync();
            return scenes
                .Where(s => s.Group == groupId)
                .Select(s => s.Name)
                .OrderBy(s => s)
                .ToArray();
        }
        
        public Task SetSceneAsync(string sceneName) => SetSceneAsync(_options.Room.Name, sceneName);

        public async Task SetSceneAsync(string groupName, string sceneName) {
            _client ??= await HueUtils.GetClient(_options.AppKey);

            //get the scene
            var (group, scene) = await GetScene(groupName, sceneName);
            
            var cmd = new SceneCommand(scene.Id);
            await _client.SendGroupCommandAsync(cmd, group.Id);
        }

        public async Task SetScheduledScene() {
            var hour = DateTime.Now.Hour;
            var entry = _options.Room.SceneSchedule
                .FirstOrDefault(s => s.From <= hour && hour < s.To);

            if (entry == null) {
                _logger.LogError("Unable to find scheduled scene for room {room}", _options.Room.Name);
                return;
            }

            await SetSceneAsync(_options.Room.Name, entry.Scene);
        }
        #endregion
        
        #region Private methods
        private async Task<Group> GetGroup(string groupName) {
            //find the group
            var groups = await _client!.GetGroupsAsync();
            return groups.FirstOrDefault(g => g.Name == groupName)
                        ?? throw new ApplicationException($"Unable to find group '{groupName}'.");
        }
        
        private async Task<(Group Group, Scene Scene)> GetScene(string groupName, string sceneName) {
            //get the group
            var group = await GetGroup(groupName);
            var groupId = group.Id;

            var scenes = await _client!.GetScenesAsync();
            var scene = scenes.FirstOrDefault(s => s.Group == groupId && s.Name == sceneName)
                   ?? throw new ApplicationException($"Unable to find scene '{sceneName}' for group '{groupName}'.");

            return (group, scene);
        }
        #endregion
    }
}