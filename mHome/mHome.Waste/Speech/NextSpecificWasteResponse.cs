using System;
using System.Linq;
using System.Threading.Tasks;
using mHome.Core.Speech.Language;
using mHome.Core.Speech.Output;
using mHome.Waste.Client;
using mHome.Waste.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace mHome.Waste.Speech {
    public class NextSpecificWasteResponse : ISpeechOutput {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _requestedWaste;

        #region Constructor methods
        public NextSpecificWasteResponse(IServiceProvider serviceProvider, string requestedWaste) {
            _serviceProvider = serviceProvider;
            _requestedWaste = requestedWaste;
        }
        #endregion
        
        #region ISpeechOutput implementation
        public async Task<string> ResolveAsync() {
            var wasteClient = _serviceProvider.GetRequiredService<IWasteClient>();
            var requestedWasteType = EnToWasteType(_requestedWaste);
            if (requestedWasteType == null) return $"I don't know what {_requestedWaste} waste is.";

            var wasteDates = await wasteClient.GetWasteAsync();
            var next = wasteDates
                .OrderBy(w => w.Date)
                .FirstOrDefault(w => w.Types.Contains(requestedWasteType.Value));
            if (next == null) return "No waste dates could be found.";

            var date = next.Date.ToString("dddd dd MMMM", LanguageUtil.GetCulture(Language.English));
            return $"The next {_requestedWaste} waste is on {date}.";
        }
        
        public bool ExpectResponse => false;
        #endregion
        
        #region Private methods
        private static WasteType? EnToWasteType(string type) {
            return type switch {
                "residual" => WasteType.Residual,
                "biological" => WasteType.Bio,
                "paper" => WasteType.Paper,
                "christmas tree" => WasteType.ChristmasTree,
                _ => null
            };
        }
        #endregion
    }
}