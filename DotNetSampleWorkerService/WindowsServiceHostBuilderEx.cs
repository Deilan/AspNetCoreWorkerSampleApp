using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;

namespace DotNetSampleWorkerService
{
    public static class WindowsServiceHostBuilderEx
    {
        // TODO: consider netcoreapp3.1- and net5.0+
        public static IHostBuilder UseWindowsServiceWithOriginalContentRoot(this IHostBuilder hostBuilder)
        {
            if (!WindowsServiceHelpers.IsWindowsService()) return hostBuilder;

            // Host.CreateDefaultBuilder uses Environment.CurrentDirectory for VS scenarios,
            // but Environment.CurrentDirectory for services is c:\Windows\System32.
            // UseWindowsService() overrides it to AppContext.BaseDirectory,
            // but in a single-file app it (usually) points to
            // %TEMP%\.net\<app>\<bundle-id>
            // learn more: https://github.com/dotnet/designs/blob/main/accepted/2020/single-file/design_3_0.md#extraction-location
            // thereby we revert it back to original application directory
            hostBuilder.UseWindowsService();
            hostBuilder.UseContentRoot(GetCurrentProcessDirectory());

            return hostBuilder;
            
            static string GetCurrentProcessDirectory()
            {
                var mainModuleFilePath = Process.GetCurrentProcess().MainModule!.FileName;
                var mainModuleDirectory = Path.GetDirectoryName(mainModuleFilePath);
                return mainModuleDirectory;
            }
        }
    }
}