using System.Linq;
using System.Threading.Tasks;
using mHome.Core.Reflection;

namespace mHome.Core.Speech.Output {
    public class CompositeSpeechUtterance : ISpeechOutput, INoResolve {
        private readonly ISpeechOutput[] _output;

        #region Constructor methods
        public CompositeSpeechUtterance(params ISpeechOutput[] output) {
            _output = output;
        }
        #endregion

        #region ISpeechUtterance implementation
        public async Task<string> ResolveAsync() {
            var @out = await Task.WhenAll(_output.Select(o => o.ResolveAsync()));
            return string.Join(" ", @out);
        }

        public bool ExpectResponse => _output.Any(o => o.ExpectResponse);
        #endregion
    }
}