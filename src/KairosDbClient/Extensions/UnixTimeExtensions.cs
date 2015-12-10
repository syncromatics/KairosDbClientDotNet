using System;

namespace KairosDbClient.Extensions
{
    public static class UnixTimeExtensions
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long MillisecondsSinceEpoch(this DateTime time)
        {
            return (long)time.ToUniversalTime().Subtract(Epoch).TotalMilliseconds;
        }

        public static DateTime DateTimeFromEpoch(this long milliseconds)
        {
            return Epoch.AddMilliseconds(milliseconds);
        }
    }
}
