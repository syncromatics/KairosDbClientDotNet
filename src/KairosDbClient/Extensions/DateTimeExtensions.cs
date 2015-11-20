using System;

namespace KairosDbClient.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1);

        public static long MillisecondsSinceEpoch(this DateTime time)
        {
            return (long)time.Subtract(Epoch).TotalMilliseconds;
        }
    }
}
