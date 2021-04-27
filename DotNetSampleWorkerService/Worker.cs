using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetSampleWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Worker> _logger;

        public Worker(IConfiguration configuration, ILogger<Worker> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var commonLogDefaultLogLevel = _configuration.GetSection("Logging:LogLevel:Default").Get<string>();
            _logger.LogTrace("Trace - Logging:LogLevel:Default is {0}", commonLogDefaultLogLevel);
            _logger.LogDebug("Debug - Logging:LogLevel:Default is {0}", commonLogDefaultLogLevel);
            _logger.LogInformation("Information - Logging:LogLevel:Default is {0}", commonLogDefaultLogLevel);
            _logger.LogWarning("Warning - Logging:LogLevel:Default is {0}", commonLogDefaultLogLevel);
            _logger.LogError("Error - Logging:LogLevel:Default is {0}", commonLogDefaultLogLevel);
            _logger.LogCritical("Critical - Logging:LogLevel:Default is {0}", commonLogDefaultLogLevel);

            var eventLogDefaultLogLevel = _configuration.GetSection("Logging:EventLog:LogLevel:Default").Get<string>();
            _logger.LogTrace("Trace - Logging:EventLog:LogLevel:Default is {0}", eventLogDefaultLogLevel);
            _logger.LogDebug("Debug - Logging:EventLog:LogLevel:Default is {0}", eventLogDefaultLogLevel);
            _logger.LogInformation("Information - Logging:EventLog:LogLevel:Default is {0}", eventLogDefaultLogLevel);
            _logger.LogWarning("Warning - Logging:EventLog:LogLevel:Default is {0}", eventLogDefaultLogLevel);
            _logger.LogError("Error - Logging:EventLog:LogLevel:Default is {0}", eventLogDefaultLogLevel);
            _logger.LogWarning("Critical - Logging:EventLog:LogLevel:Default is {0}", eventLogDefaultLogLevel);
            _logger.LogWarning("Directory.GetCurrentDirectory(): {0}", Directory.GetCurrentDirectory());
            _logger.LogWarning("Main module directory: {0}", GetMainModuleDirectory());
            _logger.LogWarning("Path.GetFullPath(\"\"): {0}", Path.GetFullPath("Logs"));
            _logger.LogWarning("AppContext.BaseDirectory: {0}", AppContext.BaseDirectory);
            _logger.LogWarning("AppDomain.CurrentDomain.BaseDirectory: {0}", AppDomain.CurrentDomain.BaseDirectory);
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        private static string GetMainModuleDirectory()
        {
            // Windows service's current directory by default points to C:\Windows\System32
            var mainModuleFileName = Process.GetCurrentProcess().MainModule!.FileName;
            var mainModuleDirectory = Path.GetDirectoryName(mainModuleFileName);
            return mainModuleDirectory;
        }
    }
}
