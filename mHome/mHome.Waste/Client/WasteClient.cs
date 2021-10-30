using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using mHome.Waste.Entity;
using mHome.Waste.Options;
using Microsoft.Extensions.Options;

namespace mHome.Waste.Client {
    public interface IWasteClient {
        Task<IEnumerable<WasteDate>> GetWasteAsync();
    }
    
    public class WasteClient : IWasteClient {
        private readonly WasteOptions _options;

        private readonly Regex _rxMonth = new("<tbody data-month=\"(?<month>\\d{1,2})-(?<year>\\d{4})\"[\\s\\S]*?</tbody>");
        private readonly Regex _rxItem = new("<tr[\\s\\S]*?<td>(?<date>[\\s\\S]*?)</td>[\\s\\S]*?<td>(?<type>[\\s\\S]*?)</td>[\\s\\S]*?</tr>");
        private readonly Regex _rxDate = new("(?<day>\\d{2})\\.(?<month>\\d{2})");
        private readonly Regex _rxType = new("<img[\\s\\S]*?title=\"(?<type>[\\w/]*?)\"[\\s\\S]*?/>");
        
        #region Constructor methods
        public WasteClient(IOptions<WasteOptions> options) {
            _options = options.Value;
        }
        #endregion
        
        #region IWasteClient implementation
        public async Task<IEnumerable<WasteDate>> GetWasteAsync() {
            var client = new HttpClient();
            var document = await client.GetStringAsync(_options.ServiceUrl);

            var wasteDates = new List<WasteDate>();
            var monthMatches = _rxMonth.Matches(document);
            foreach (var monthMatch in monthMatches) {
                var mm = (monthMatch as Match)!;
                if (!mm.Success) continue;
                
                var _ = Convert.ToInt32(mm.Groups["month"].Value); //may also be used
                var year = Convert.ToInt32(mm.Groups["year"].Value);

                var itemMatches = _rxItem.Matches(mm.Value);
                foreach (var itemMatch in itemMatches) {
                    var im = (itemMatch as Match)!;
                    if(!im.Success) continue;

                    var dateMatch = _rxDate.Match(im.Groups["date"].Value);
                    if (!dateMatch.Success) continue;
                    var day = Convert.ToInt32(dateMatch.Groups["day"].Value);
                    var month2 = Convert.ToInt32(dateMatch.Groups["month"].Value);
                    var wasteDate = new WasteDate(new DateTime(year, month2, day));
                    
                    var typeMatches = _rxType.Matches(im.Groups["type"].Value);
                    foreach (var typeMatch in typeMatches) {
                        var tm = (typeMatch as Match)!;
                        if (!tm.Success) continue;
                        var type = tm.Groups["type"].Value;
                        wasteDate.Types.Add(ParseType(type));
                    }
                    wasteDates.Add(wasteDate);
                }
            }

            return wasteDates;
        }
        #endregion
        
        #region Private methods
        private static WasteType ParseType(string type) {
            return type switch {
                "Restavfall" => WasteType.Residual,
                "Bio" => WasteType.Bio,
                "Papp/papir" => WasteType.Paper,
                "Juletre" => WasteType.ChristmasTree,
                _ => WasteType.Unknown
            };
        }
        #endregion
    }
}