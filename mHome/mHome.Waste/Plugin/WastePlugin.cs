using System.Reflection;
using mHome.Core.Plugin;
using mHome.Core.Reflection;
using mHome.Core.Speech.Input;
using mHome.Core.Speech.Mediation;
using mHome.Waste.Client;
using mHome.Waste.Options;
using mHome.Waste.Speech;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace mHome.Waste.Plugin {
    public class WastePlugin : IPlugin {
        #region IPlugin implementation
        public void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services) {
            //configuration
            services.Configure<WasteOptions>(hostContext.Configuration.GetSection(WasteOptions.SectionName));

            //waste client
            services.AddSingleton<IWasteClient, WasteClient>();

            //speech
            services.AddSingleton<ITypeResolver<ISpeechInput>>(_ => new TypeResolver<ISpeechInput>(Assembly.GetExecutingAssembly()));
            services.AddSingleton<IRequestResponseMediator, WasteSpeechMediator>();
        }
        #endregion
    }
}