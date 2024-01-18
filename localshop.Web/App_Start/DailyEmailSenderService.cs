// DailyEmailSenderService.cs
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

public class DailyEmailSenderService : IHostedService, IDisposable
{
    private Timer _timer;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Set up the timer to trigger daily at 10 am
        DateTime now = DateTime.Now;
        DateTime scheduledTime = new DateTime(now.Year, now.Month, now.Day, 11, 34, 0);
        TimeSpan timeUntil10AM = scheduledTime > now ? scheduledTime - now : TimeSpan.FromHours(24) - (now - scheduledTime);

        _timer = new Timer(DoWork, null, timeUntil10AM, TimeSpan.FromHours(24));

        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        // Call the function to send the daily email
        SendDailyEmail();
    }

    private void SendDailyEmail()
    {
        // Code to send the daily email
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
