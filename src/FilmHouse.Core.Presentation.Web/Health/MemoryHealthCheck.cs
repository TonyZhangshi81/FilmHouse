using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FilmHouse.Core.Presentation.Web.Health;

public class MemoryHealthCheck : IHealthCheck
{
    private readonly IConfiguration _configuration;
    private readonly MemoryOption _memoryOptions = new MemoryOption();

    public MemoryHealthCheck(IConfiguration configuration)
    {
        this._configuration = configuration;

        this._configuration.GetSection("Memory").Bind(_memoryOptions);
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var drive = AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[0];
            var metrics = this.GetMetrics();
            var percentUsed = 100 * metrics.Used / metrics.Total;

            var status = HealthStatus.Healthy;
            if (percentUsed > _memoryOptions.PercentUsedDegraded)
            {
                status = HealthStatus.Degraded;
            }
            if (percentUsed > _memoryOptions.PercentUsedUnhealthy)
            {
                status = HealthStatus.Unhealthy;
            }

            var result = new HealthCheckResult(status,
                                description: string.Format(_memoryOptions.Description, _memoryOptions.PercentUsedDegraded, _memoryOptions.PercentUsedUnhealthy, _memoryOptions.PercentUsedUnhealthy),
                                exception: null,
                                data: new Dictionary<string, object>() {
                                    { "PercentUsedDegraded", _memoryOptions.PercentUsedDegraded },
                                    { "PercentUsedUnhealthy", _memoryOptions.PercentUsedUnhealthy },
                                    { "PercentUsed", percentUsed }
                                });
            return Task.FromResult(result);
        }
        catch (Exception ex)
        {
            var result = HealthCheckResult.Unhealthy(_memoryOptions.UnhealthyMessage, ex);

            return Task.FromResult(result);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected virtual MemoryMetrics GetMetrics()
    {
        MemoryMetrics metrics;

        // 操作系統判定
        if (this.IsUnix())
        {
            // LINUX,OSX
            metrics = GetUnixMetrics();
        }
        else
        {
            // WINDOWS
            metrics = GetWindowsMetrics();
        }

        return metrics;
    }

    /// <summary>
    /// 操作系統判定
    /// </summary>
    /// <returns>windows系統: return false</returns>
    private bool IsUnix()
    {
        var isUnix = RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
                     RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        return isUnix;
    }

    /// <summary>
    /// LINUX,OSX環境下的系統內存狀態
    /// </summary>
    /// <returns></returns>
    private MemoryMetrics GetUnixMetrics()
    {
        var output = "";

        var info = new ProcessStartInfo("free -m")
        {
            FileName = "/bin/bash",
            Arguments = "-c \"free -m\"",
            RedirectStandardOutput = true
        };

        using (var process = Process.Start(info))
        {
            output = process.StandardOutput.ReadToEnd();
            Console.WriteLine(output);
        }

        var lines = output.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        var memory = lines[1].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

        var metrics = new MemoryMetrics
        {
            Total = double.Parse(memory[1]),
            Used = double.Parse(memory[2]),
            Free = double.Parse(memory[3])
        };

        return metrics;
    }

    /// <summary>
    /// WINDOWS環境下的系統內存狀態
    /// </summary>
    /// <returns></returns>
    private MemoryMetrics GetWindowsMetrics()
    {
        var output = "";

        var info = new ProcessStartInfo
        {
            FileName = "wmic",
            Arguments = "OS get FreePhysicalMemory,TotalVisibleMemorySize /Value",
            RedirectStandardOutput = true,
            UseShellExecute = false
        };

        using (var process = Process.Start(info))
        {
            output = process.StandardOutput.ReadToEnd();
        }

        var lines = output.Trim().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        var freeMemoryParts = lines[0].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
        var totalMemoryParts = lines[1].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);

        var metrics = new MemoryMetrics
        {
            Total = Math.Round(double.Parse(totalMemoryParts[1]) / 1024, 0),
            Free = Math.Round(double.Parse(freeMemoryParts[1]) / 1024, 0)
        };
        metrics.Used = metrics.Total - metrics.Free;

        return metrics;
    }

    /// <summary>
    /// 系統內存狀態
    /// </summary>
    public class MemoryMetrics
    {
        /// <summary>
        /// 總量
        /// </summary>
        public double Total;
        /// <summary>
        /// 已使用
        /// </summary>
        public double Used;
        /// <summary>
        /// 未使用
        /// </summary>
        public double Free;
    }
}

public class MemoryOption
{
    public string FriendlyName { get; set; }

    public string Description { get; set; }

    public string UnhealthyMessage { get; set; }

    public int PercentUsedDegraded { get; set; }

    public int PercentUsedUnhealthy { get; set; }
}