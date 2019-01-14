using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace PersonalOrganizerApp.Scheduler
{
    public class ScheduledJob : IJob
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<ScheduledJob> logger;

        public ScheduledJob(IConfiguration configuration, ILogger<ScheduledJob> logger)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            logger.LogWarning($"Hello scheduled task {DateTime.Now.ToLongTimeString()}");
            await Task.CompletedTask;
        }
    }
}
