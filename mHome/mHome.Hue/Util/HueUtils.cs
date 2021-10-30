using System;
using System.Linq;
using System.Threading.Tasks;
using Q42.HueApi;
using Q42.HueApi.Interfaces;

namespace mHome.Hue.Util {
    public static class HueUtils {
        public static async Task<IHueClient> GetClient(string appKey) {
            //locate the bridge
            var locator = new HttpBridgeLocator();
            var bridges = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(5));
            var bridge = bridges.First();

            //create and initialize the client
            var client = new LocalHueClient(bridge.IpAddress);
            client.Initialize(appKey);

            return client;
        }
    }
}