using System.Diagnostics;

namespace DiscordBot.Services
{
    public class UptimeService
    {
        private readonly Stopwatch _stopwatch;

        public UptimeService()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public TimeSpan GetUptime()
        {
            return _stopwatch.Elapsed;
        }
    }
}
