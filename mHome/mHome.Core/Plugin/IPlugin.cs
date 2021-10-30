using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace mHome.Core.Plugin {
    public interface IPlugin {
        void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services);
    }
}