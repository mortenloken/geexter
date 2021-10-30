using mHome.Bot.Options;
using mHome.Core.Plugin;
using mHome.Core.Reflection;
using mHome.Core.Speech.Bot;
using mHome.Core.Speech.Input;
using mHome.Core.Speech.Mediation;
using mHome.Core.Voice;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace mHome.Bot.Plugin {
    public class BotPlugin : IPlugin {
        #region IPlugin implementation
        public void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services) {
            //configuration
            services.Configure<BotOptions>(hostContext.Configuration.GetSection(BotOptions.SectionName));

            //bot
            services.AddSingleton<IVoiceService>(_ => new VoiceService(Voice.Ryan));
            services.AddSingleton<ISpeechInputProcessor, SpeechInputProcessor>();
            services.AddSingleton<ISpeechBot, SpeechBot>();
            
            //speech
            services.AddSingleton<ITypeResolver<ISpeechInput>>(_ => new TypeResolver<ISpeechInput>(typeof(ISpeechBot).Assembly)); //load default speech from core
            services.AddSingleton<IRequestResponseMediator, DefaultRequestResponseMediator>();
        }
        #endregion
    }
}