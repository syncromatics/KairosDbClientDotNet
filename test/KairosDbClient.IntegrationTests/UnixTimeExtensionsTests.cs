using System;
using FluentAssertions;
using KairosDbClient.Extensions;
using Xunit;

namespace KairosDbClient.IntegrationTests
{
    public class UnixTimeExtensionsTests
    {
        [Fact]
        public void Can_round_trip_date_time_to_milliseconds()
        {
            var time = DateTime.Now;

            var converted = time.MillisecondsSinceEpoch().DateTimeFromEpoch();

            converted
                .ToLocalTime()
                .Should().BeCloseTo(time);
        }
    }
}
