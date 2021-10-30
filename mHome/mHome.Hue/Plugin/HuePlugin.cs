using System.Reflection;
using mHome.Core.Lights;
using mHome.Core.Plugin;
using mHome.Core.Reflection;
using mHome.Core.Speech.Input;
using mHome.Core.Speech.Mediation;
using mHome.Hue.Mediation;
using mHome.Hue.Options;
using mHome.Hue.Speech;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace mHome.Hue.Plugin {
    public class HuePlugin : IPlugin {
        #region IPlugin implementation
        public void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services) {
            //configuration
            services.Configure<HueOptions>(hostContext.Configuration.GetSection(HueOptions.SectionName));
            
            //hue / lights
            services.AddSingleton<ILightsManager, HueManager>();
            services.AddSingleton<IHueMediator, HueMediator>();
            services.AddHostedService<HueGroupMonitor>();

            //speech
            services.AddSingleton<ITypeResolver<ISpeechInput>>(_ => new TypeResolver<ISpeechInput>(Assembly.GetExecutingAssembly()));
            services.AddSingleton<IRequestResponseMediator, HueSpeechMediator>();
        }
        #endregion
    }
}