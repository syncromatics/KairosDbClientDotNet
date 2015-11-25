using System.Collections.Generic;
using KairosDbClient.Aggregators;
using KairosDbClient.Grouping;
using Newtonsoft.Json;

namespace KairosDbClient
{
    /// <summary>
    /// Use to query a metric by name, optionally including group bys or aggregators
    /// </summary>
    public class QueryMetric
    {
        public string Name { get; set; }
        private readonly Dictionary<string, string> _tags = new Dictionary<string, string>();
        private List<Aggregator> _aggregators;
        private readonly List<GroupBy> _groupBys = new List<GroupBy>();

        public IReadOnlyDictionary<string, string> Tags => _tags;
        public IReadOnlyList<Aggregator> Aggregators => _aggregators;

        [JsonProperty("group_by")]
        public IReadOnlyList<GroupBy> GroupBy => _groupBys; 

        public QueryMetric(string name)
        {
            Name = name;
        }

        public QueryMetric AddAggregator(Aggregator aggregator)
        {
            if (_aggregators == null)
            {
                _aggregators = new List<Aggregator>();
            }
            _aggregators.Add(aggregator);
            return this;
        }

        public QueryMetric AddTag(string name, string value)
        {
            _tags[name] = value;
            return this;
        }

        public QueryMetric AddGroupBy(GroupBy groupBy)
        {
            _groupBys.Add(groupBy);
            return this;
        }
    }
}
