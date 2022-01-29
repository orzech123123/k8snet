using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;

namespace react_app.BackgroundTasks
{
    [DisallowConcurrentExecution]
    public class TruncateLogsBackgroundJob : IJob
    {
        private readonly ILogger<TruncateLogsBackgroundJob> _logger;
        private readonly IServiceProvider serviceProvider;

        public TruncateLogsBackgroundJob(
            ILogger<TruncateLogsBackgroundJob> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            this.serviceProvider = serviceProvider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
          
        }
    }
}
