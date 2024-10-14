using Application.Services.Hangfire.Interfaces;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Application.Services.Hangfire.Implementations;

public class HangfireService : IHangfireService
{
    private readonly IBackgroundJobClient _backgroundJobs;
    private readonly IRecurringJobManager _recurringJobs;
    private readonly ILogger<HangfireService> _logger;

    public HangfireService(IBackgroundJobClient backgroundJobs, IRecurringJobManager recurringJobs,
        ILogger<HangfireService> logger)
    {
        _recurringJobs = recurringJobs;
        _backgroundJobs = backgroundJobs;
        _logger = logger;
    }

    public void DefaultTask()
    {
        _logger.LogInformation("Serilog Work!");
        _recurringJobs.AddOrUpdate("default-task", () => HangfireLog(), Cron.Minutely());
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public void HangfireLog()
    {
        _logger.LogInformation("Hangfire Work!");
    }
}