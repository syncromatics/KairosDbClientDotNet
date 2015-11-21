using System.Collections.Generic;
using System.Threading.Tasks;
using KairosDbClient.Response;

namespace KairosDbClient
{
    public interface IKairosClient
    {
        Task AddMetricsAsync(IEnumerable<Metric> metrics);
        Task<QueryResponse> QueryMetricsAsync(QueryBuilder query);
    }
}