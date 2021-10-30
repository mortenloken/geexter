using mHome.Core.Plugin;
using mHome.KeywordWaker.Mediation;
using mHome.KeywordWaker.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace mHome.KeywordWaker.Plugin {
    public class KeywordWakerPlugin : IPlugin {
        #region IPlugin implementation
        public void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services) {
            //configuration
            services.Configure<KeywordWakerOptions>(hostContext.Configuration.GetSection(KeywordWakerOptions.SectionName));
            
            //keyword waker
            services.AddSingleton<IKeywordMediator, KeywordMediator>();
            services.AddHostedService<KeywordWaker>();
        }
        #endregion
    }
}