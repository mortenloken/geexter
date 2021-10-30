// using mHome.SBanken;
// using mHome.SBanken.Options;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Options;
// using NUnit.Framework;
//
// namespace mHome.Test {
//     public class SBankenTests {
//         private const string AppSettingsPath = @"C:\Dev\VSO\Geexter\mHome\mHome\appsettings.json";
//         private SBankenClient _client = null!;
//         
//         [SetUp]
//         public void Setup() {
//             var config = new ConfigurationBuilder()
//                 .AddJsonFile(AppSettingsPath)
//                 .Build();
//
//             var options = new SBankenOptions();
//             config.GetSection("SBanken").Bind(options);
//
//             _client = new SBankenClient(Options.Create(options));
//         }
//     }
// }