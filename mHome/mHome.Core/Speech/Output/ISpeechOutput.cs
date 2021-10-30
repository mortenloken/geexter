using System.Threading.Tasks;

namespace mHome.Core.Speech.Output {
    public interface ISpeechOutput : IOutput {
        bool ExpectResponse { get; }
        Task<string> ResolveAsync();
    }
}