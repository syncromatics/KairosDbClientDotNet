using KairosDbClient.Aggregators;
using Newtonsoft.Json;

namespace KairosDbClient
{
    /// <summary>
    /// A relative time with a value and unit (e.g. seconds, years, etc)
    /// </summary>
    public class RelativeTime
    {
        public long Value { get; set; }
        [JsonIgnore]
        public TimeUnit Unit { get; set; }

        [JsonProperty("unit")]
        public string UnitString => Unit.ToString();

        public RelativeTime(long value, TimeUnit unit)
        {
            Value = value;
            Unit = unit;
        }
    }
}
