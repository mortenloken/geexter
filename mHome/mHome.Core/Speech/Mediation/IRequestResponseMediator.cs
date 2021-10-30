using System.Collections.Generic;
using mHome.Core.Mediation;
using mHome.Core.Speech.Input;
using mHome.Core.Speech.Output;

namespace mHome.Core.Speech.Mediation {
    public interface IRequestResponseMediator : IMediator<ISpeechInput, IEnumerable<IOutput>> { }
}