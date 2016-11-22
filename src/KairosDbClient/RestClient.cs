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
        private static readonly HttpClient HttpClient;

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

        static RestClient()
        {
            HttpClient = new HttpClient();
        }

        public async Task AddMetricsAsync(IEnumerable<Metric> metrics)
        {
            var serialized = JsonConvert.SerializeObject(metrics, _settings);
            var response = await HttpClient.PostAsync($"{_baseUrl}/api/v1/datapoints", new StringContent(serialized));
            if (!response.IsSuccessStatusCode)
            {
                await ThrowOnError(response);
            }
            
        }

        public async Task<QueryResponse> QueryMetricsAsync(QueryBuilder query)
        {
            var serialized = JsonConvert.SerializeObject(query, _settings);
            var response = await HttpClient.PostAsync($"{_baseUrl}/api/v1/datapoints/query", new StringContent(serialized)).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                await ThrowOnError(response);
            }
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<QueryResponse>(content);
        }

        public async Task<QueryResponse> DeleteMetricAsync(string metric)
        {
            var response = await HttpClient.DeleteAsync($"{_baseUrl}/api/v1/metric/{metric}").ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                await ThrowOnError(response);
            }
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<QueryResponse>(content);
        }

        public async Task<QueryResponse> DeleteMetricsAsync(QueryBuilder query)
        {
            var serialized = JsonConvert.SerializeObject(query, _settings);
            var response = await HttpClient.PostAsync($"{_baseUrl}/api/v1/datapoints/delete", new StringContent(serialized)).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                await ThrowOnError(response);
            }
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<QueryResponse>(content);
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
