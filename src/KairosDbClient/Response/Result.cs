using System.Collections.Generic;
using System.Linq;

namespace KairosDbClient.Response
{
    /// <summary>
    /// The metric name, tags, and datapoints resulting from a query
    /// </summary>
    public class Result
    {
        public string Name { get; set; }
        public Dictionary<string, List<string>> Tags { get; set; }
        public List<List<object>> Values { get; set; }

        public IEnumerable<DataPoint> DataPoints => Values.Select(value => new DataPoint((long)value[0], value[1]));
    }
}