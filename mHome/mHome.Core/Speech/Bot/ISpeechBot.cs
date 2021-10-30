using System;
using System.Threading.Tasks;
using mHome.Core.Speech.Output;

namespace mHome.Core.Speech.Bot {
    public interface ISpeechBot : IDisposable {
        Task SpeakAsync(string text);
        Task SpeakAsync(params ISpeechOutput[] output);
        Task SpeakAsync(ISpeechOutput output);
    }
}