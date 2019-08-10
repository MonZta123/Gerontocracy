using System;
using System.Threading;
using System.Threading.Tasks;
using Gerontocracy.Core.Config;
using Gerontocracy.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Gerontocracy.Core.HostedServices
{
    public class SyncHostedService : IHostedService
    {
        public SyncHostedService(
            IServiceProvider services,
            ILogger<SyncHostedService> logger)
        {
            _services = services;
            _logger = logger;
        }

        private readonly IServiceProvider _services;
        private readonly ILogger _logger;

        private Timer _parlamentTimer;
        private Timer _newsTimer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");
            
            _parlamentTimer = new Timer(TriggerParlamentSyncJob, null, TimeSpan.Zero, TimeSpan.FromDays(1));
            _newsTimer = new Timer(TriggerNewsSyncJob, null, TimeSpan.Zero, TimeSpan.FromMinutes(30));

            return Task.CompletedTask;
        }

        private void TriggerParlamentSyncJob(object state)
        {
            _logger.LogInformation("Parlament Background Service is working.");

            using (var scope = _services.CreateScope())
            {
                var syncService =
                    scope.ServiceProvider
                        .GetRequiredService<ISyncService>();
                
                syncService.SyncPolitiker();
            }
        }

        private void TriggerNewsSyncJob(object state)
        {
            _logger.LogInformation("News Background Service is working.");

            using (var scope = _services.CreateScope())
            {
                var syncService =
                    scope.ServiceProvider
                        .GetRequiredService<ISyncService>();

                syncService.SyncApa();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _parlamentTimer?.Change(Timeout.Infinite, 0);
            _newsTimer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
