using System.Threading.Tasks;
using JetBrains.Annotations;

namespace mHome.Core.Lights {
	[PublicAPI]
    public interface ILightsManager {
        /// <summary>
        /// Turns light on / off for the default room.
        /// </summary>
        Task LightsOnOffAsync(bool on);

        /// <summary>
        /// Turns light on / off for the specified room.
        /// </summary>
        Task LightsOnOffAsync(string groupName, bool on);

        /// <summary>
        /// Gets the scenes in the default room.
        /// </summary>
        Task<string[]> GetScenesAsync();

        /// <summary>
        /// Sets the specified scene for the default room.
        /// </summary>
        Task SetSceneAsync(string sceneName);

        /// <summary>
        /// Sets the specified scene for the specified room.
        /// </summary>
        Task SetSceneAsync(string groupName, string sceneName);
        
        /// <summary>
        /// Sets the scheduled scene for the default room.
        /// </summary>
        Task SetScheduledScene();
    }
}