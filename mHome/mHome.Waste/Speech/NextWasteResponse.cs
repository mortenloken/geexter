using System;
using System.Linq;
using System.Threading.Tasks;
using mHome.Core.Speech.Language;
using mHome.Core.Speech.Output;
using mHome.Waste.Client;
using mHome.Waste.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace mHome.Waste.Speech {
    public class NextWasteResponse : ISpeechOutput {
        private readonly IServiceProvider _serviceProvider;

        #region Constructor methods
        public NextWasteResponse(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }
        #endregion

        #region ISpeechOutput implementation
        public async Task<string> ResolveAsync() {
            var wasteClient = _serviceProvider.GetRequiredService<IWasteClient>();
            var wasteDates = await wasteClient.GetWasteAsync();
            var next = wasteDates
                .OrderBy(w => w.Date)
                .FirstOrDefault(w => w.Date >= DateTime.Today);
            if (next == null) return "No waste dates could be found.";

            var date = next.Date.ToString("dddd dd MMMM", LanguageUtil.GetCulture(Language.English));
            return $"On {date}, please take out the {string.Join(" and ", next.Types.Select(WasteTypeToEn))}";
        }
        
        public bool ExpectResponse => false;
        #endregion
        
        #region Private methods
        private static string WasteTypeToEn(WasteType wt) {
            return wt switch {
                WasteType.Residual => "residual waste",
                WasteType.Bio => "biological waste",
                WasteType.Paper => "paper waste",
                WasteType.ChristmasTree => "christmas tree",
                WasteType.Unknown => "unknown waste",
                _ => "something nasty"
            };
        }
        #endregion
    }
}