using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FilmHouse.Mvc.Health;

public class DiskSpaceHealthCheck : IHealthCheck
{
    private readonly IConfiguration _configuration;
    private readonly DiskSpaceOption _diskSpaceOptions = new DiskSpaceOption();

    public DiskSpaceHealthCheck(IConfiguration configuration)
    {
        this._configuration = configuration;

        this._configuration.GetSection("DiskSpace").Bind(_diskSpaceOptions);
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var drive = AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[0];

            var percentageFree = this.GetPercentageFree(drive);

            var result = new HealthCheckResult(
                                status: this.DetermineState(percentageFree),
                                description: string.Format(_diskSpaceOptions.Description, _diskSpaceOptions.MinPercentageFree),
                                exception: null,
                                data: new Dictionary<string, object>() {
                                    { "MinPercentageFree", _diskSpaceOptions.MinPercentageFree },
                                    { "WarningPercentageFree", _diskSpaceOptions.WarningPercentageFree },
                                    { "PercentageFree", percentageFree }
                                });
            return Task.FromResult(result);
        }
        catch (Exception ex)
        {
            var result = HealthCheckResult.Unhealthy(_diskSpaceOptions.UnhealthyMessage, ex);

            return Task.FromResult(result);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="drive"></param>
    /// <returns></returns>
    private double GetPercentageFree(string drive)
    {
        var driveInfo = new DriveInfo(drive);
        var freeSpace = driveInfo.TotalFreeSpace;
        var totalSpace = driveInfo.TotalSize;
        var percentageFree = Convert.ToDouble(freeSpace) / Convert.ToDouble(totalSpace) * 100;
        return percentageFree;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="percentageFree"></param>
    /// <returns></returns>
    private HealthStatus DetermineState(double percentageFree)
    {
        if (percentageFree < this._diskSpaceOptions.MinPercentageFree)
        {
            return HealthStatus.Unhealthy;
        }
        if (percentageFree < this._diskSpaceOptions.WarningPercentageFree)
        {
            return HealthStatus.Degraded;
        }
        return HealthStatus.Healthy;
    }
}

public class DiskSpaceOption
{
    public string FriendlyName { get; set; }

    public string Description { get; set; }

    public string UnhealthyMessage { get; set; }

    public int MinPercentageFree { get; set; }

    public int WarningPercentageFree { get; set; }
}