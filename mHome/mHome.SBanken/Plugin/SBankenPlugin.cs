using System.Reflection;
using mHome.Core.Plugin;
using mHome.Core.Reflection;
using mHome.Core.Speech.Input;
using mHome.Core.Speech.Mediation;
using mHome.SBanken.Options;
using mHome.SBanken.Speech;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace mHome.SBanken.Plugin {
    public class SBankenPlugin : IPlugin {
        #region IPlugin implementation
        public void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services) {
            //configuration
            services.Configure<SBankenOptions>(hostContext.Configuration.GetSection(SBankenOptions.SectionName));

            //sbanken client
            services.AddSingleton<ISBankenClient, SBankenClient>();

            //speech
            services.AddSingleton<ITypeResolver<ISpeechInput>>(_ => new TypeResolver<ISpeechInput>(Assembly.GetExecutingAssembly()));
            services.AddSingleton<IRequestResponseMediator, SBankenSpeechMediator>();
        }
        #endregion
    }
}