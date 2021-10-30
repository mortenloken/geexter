using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using mHome.Hue.Mediation;
using mHome.Hue.Options;
using mHome.Hue.Util;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Q42.HueApi.Models.Groups;

namespace mHome.Hue {
    public class HueGroupMonitor : IHostedService, IDisposable {
        private readonly HueOptions _options;
        private readonly IHueMediator _mediator;
        private readonly ILogger<HueGroupMonitor> _logger;
        private readonly CancellationTokenSource _cts;

        #region Constructor methods
        public HueGroupMonitor(IHueMediator mediator, IOptions<HueOptions> options, ILogger<HueGroupMonitor> logger) {
            _mediator = mediator;
            _options = options.Value;
            _logger = logger;
            _cts = new CancellationTokenSource();
        }
        #endregion
        
        #region IHostedService immplementation
        public async Task StartAsync(CancellationToken cancellationToken) {
            var client = await HueUtils.GetClient(_options.AppKey);

            //find the group
            var groupName = _options.Room.Name;
            _logger.LogInformation("Initializing room monitor...");
            var groups = await client!.GetGroupsAsync();
            var grp = groups.FirstOrDefault(g => g.Name == groupName) ?? throw new ApplicationException($"Unable to find group '{groupName}'.");
            var groupId = grp.Id;

            //never care about the reference to this task
            _ = Task.Factory.StartNew(async () => {
                _logger.LogInformation($"Starting monitoring group '{groupName}'...");
                GroupState? state = null;
                while (true) {
                    //check cancellation
                    if (_cts.IsCancellationRequested) break;

                    //get the current state
                    var group = await client.GetGroupAsync(groupId);
                    var newState = group!.State;

                    //compare with previous
                    if (state != null) {
                        //check if lights have been turned on or off
                        if ((state.AnyOn ?? false) != (newState.AnyOn ?? false)) {
                            if (newState.AnyOn ?? false) {
                                _logger.LogDebug("Lights on detected.");
                                await _mediator.MediateAsync(HueEvent.LightsOn);
                            }
                            else {
                                _logger.LogInformation("Lights off detected.");
                                await _mediator.MediateAsync(HueEvent.LightsOff);
                            }
                        }
                    }

                    //continue iteration
                    state = newState;
                    await Task.Delay(TimeSpan.FromSeconds(1), _cts.Token);
                }
            }, _cts.Token);
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            //cancel the service's cancellation token
            _cts.Cancel();
            return Task.CompletedTask;
        }
        #endregion
        
        #region IDisposable implementation
        public void Dispose() {
	        _cts.Dispose();
	        GC.SuppressFinalize(this);
        }
        #endregion
    }
}