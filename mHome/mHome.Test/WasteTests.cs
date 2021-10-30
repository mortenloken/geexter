using System.Linq;
using System.Threading.Tasks;
using mHome.Waste.Client;
using mHome.Waste.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace mHome.Test {
    public class WasteTests {
        private const string AppSettingsPath = @"C:\Dev\VSO\Geexter\mHome\mHome\appsettings.json";
        private WasteClient _client = null!;
        
        [SetUp]
        public void Setup() {
            var config = new ConfigurationBuilder()
                .AddJsonFile(AppSettingsPath)
                .Build();

            var options = new WasteOptions();
            config.GetSection("Waste").Bind(options);
        
            _client = new WasteClient(Options.Create(options));
        }

        [Test]
        public async Task GetWaste() {
            var wasteDates = await _client.GetWasteAsync();
            Assert.True(wasteDates.Any());
        }
    }
}