using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KairosDbClient.Error;
using KairosDbClient.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace KairosDbClient
{
    public class RestClient : IKairosClient
    {
        private readonly string _baseUrl;

        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            //everything is good as long as the property name doesn't have more than two words
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        public RestClient(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task AddMetricsAsync(IEnumerable<Metric> metrics)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var serialized = JsonConvert.SerializeObject(metrics, _settings);
                var response = await client.PostAsync("/api/v1/datapoints", new StringContent(serialized));
                if (!response.IsSuccessStatusCode)
                {
                    await ThrowOnError(response);
                }
            }
        }

        public async Task<QueryResponse> QueryMetricsAsync(QueryBuilder query)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var serialized = JsonConvert.SerializeObject(query, _settings);
                var response = await client.PostAsync("/api/v1/datapoints/query", new StringContent(serialized));
                if (!response.IsSuccessStatusCode)
                {
                    await ThrowOnError(response);
                }
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<QueryResponse>(content);
            }
        }

        private async Task ThrowOnError(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(content);
                throw new BadRequestException("KairosDb returned status code 400: Bad Request.", errorResponse.Errors);
            }
            response.EnsureSuccessStatusCode();
        }
    }
}
