using System.Threading.Tasks;

namespace mHome.Core.Mediation {
    public interface IMediator<in TInput> {
        Task MediateAsync(TInput input);
    }
    public interface IMediator<in TInput, TOutput> where TOutput : class {
        Task<TOutput?> MediateAsync(TInput input);
    }
}