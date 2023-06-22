using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWatcher.BackgroundServices.ImportArchives
{
    public class ArchiveImportHostedService : BackgroundService
    {
        public ImportArchiveBackgroundTaskQueue TaskQueue { get; }
        private readonly ILogger<ArchiveImportHostedService> _logger;

        public ArchiveImportHostedService(ImportArchiveBackgroundTaskQueue taskQueue,
            ILogger<ArchiveImportHostedService> logService)
        {
            TaskQueue = taskQueue;
            _logger = logService;
        }


        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var workItem = await TaskQueue.DequeueAsync(cancellationToken);
                var sw = new Stopwatch();
                try
                {
                    sw.Start();
                    _logger.LogInformation("Started a ArchiveImportHostedService");
                    await workItem(cancellationToken);
                    sw.Stop();
                    _logger.LogInformation($"Ended with success a ArchiveImportHostedService, took {sw.Elapsed} time");
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Ended with error a ArchiveImportHostedService, took {sw.Elapsed} time");
                    _logger.LogError(ex, ex.Message);
                }
            }
        }
    }
}
