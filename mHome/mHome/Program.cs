using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using mHome.Bot.Plugin;
using mHome.Core.Lights;
using mHome.Core.Plugin;
using mHome.Core.Speech.Bot;
using mHome.Hue.Plugin;
using mHome.KeywordWaker.Plugin;
using mHome.SBanken.Plugin;
using mHome.Waste.Plugin;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace mHome {
	[UsedImplicitly]
    public class Program {
        //todo - plugins should be resolved dynamically somehow
        private static readonly IPlugin[] Plugins = {
            new WastePlugin(),
            new SBankenPlugin(),
            new HuePlugin(),
            new KeywordWakerPlugin(),
            new BotPlugin()
        };
        
        public static Task Main(string[] args) {
            //setup serilog
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();            
            
            //run host
            return CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) {
            return Host.CreateDefaultBuilder(args)
                .UseConsoleLifetime()
                .UseSerilog(Log.Logger)
                .ConfigureServices((hostContext, services) =>
                {

                    //configure plugins
                    foreach (var plugin in Plugins) {
                        try {
                            plugin.ConfigureServices(hostContext, services);
                        }
                        catch (Exception ex) {
                            Log.Logger.Error(ex, "Unable to load plugin {plugin]", plugin.GetType().Name);
                        }
                    }
                    
                    //add default services which may be other services may be dependent on
                    services.TryAddSingleton<ISpeechBot, NullSpeechBot>();
                    services.TryAddSingleton<ILightsManager, NullLightsManager>();
                });
        }
    }
}