using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace KairosDbClient
{
    /// <summary>
    /// a metric with a name, tags, and datapoints for pushing data into kairosdb
    /// </summary>
    public class Metric
    {
        public string Name { get; private set; }
        private readonly Dictionary<string, string> _tags = new Dictionary<string, string>();
        private readonly List<DataPoint> _dataPoints = new List<DataPoint>();
        /// <summary>
        /// The type name as registered on the server if this is a custom type
        /// </summary>
        public string Type { get; private set; }

        [JsonIgnore]
        public IReadOnlyList<DataPoint> DataPoints => _dataPoints;

        /// <summary>
        /// The data points as a two item array for serialization
        /// </summary>
        [JsonProperty("datapoints")]
        private IEnumerable<object[]> ArrayDataPoints => _dataPoints.Select(point => new [] {point.Timestamp, point.Value});

        public IReadOnlyDictionary<string, string> Tags => _tags;

        public Metric(string name)
        {
            Name = name;
        }

        public Metric AddTag(string name, string value)
        {
            _tags[name] = value;
            return this;
        }

        public Metric AddTags(IDictionary<string, string> tags)
        {
            foreach (var tag in tags)
            {
                _tags[tag.Key] = tag.Value;
            }

            return this;
        }

        public Metric AddDataPoint(DataPoint dataPoint)
        {
            _dataPoints.Add(dataPoint);
            return this;
        }

        public Metric AddDataPoints(IEnumerable<DataPoint> dataPoints)
        {
            _dataPoints.AddRange(dataPoints);
            return this;
        }

        public Metric SetType(string type)
        {
            Type = type;
            return this;
        }
    }
}
