using System.Collections.Generic;
using Newtonsoft.Json;

namespace KairosDbClient.Response
{
    public class Query
    {
        [JsonProperty("sample_size")]
        public long SampleSize { get; set; }
        public List<Result> Results { get; set; }
    }
}