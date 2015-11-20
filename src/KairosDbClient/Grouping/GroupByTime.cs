using KairosDbClient.Aggregators;
using Newtonsoft.Json;

namespace KairosDbClient.Grouping
{
    public class GroupByTime : GroupBy
    {
        [JsonProperty("range_size")]
        public Sampling RangeSize { get; private set; }
        [JsonProperty("group_count")]
        public int GroupCount { get; private set; }

        public GroupByTime(Sampling sampling, int groupCount) : base("time")
        {
            RangeSize = sampling;
            GroupCount = groupCount;
        }
    }
}