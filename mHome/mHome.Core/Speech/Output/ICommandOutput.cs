using System.Threading.Tasks;

namespace mHome.Core.Speech.Output {
    public interface ICommandOutput : IOutput {
        Task InvokeAsync();
    }
}