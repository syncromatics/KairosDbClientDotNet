using System;
using System.Linq;
using System.Threading;
using FluentAssertions;
using KairosDbClient.Aggregators;
using KairosDbClient.Aggregators.Range;
using KairosDbClient.Extensions;
using KairosDbClient.Grouping;
using Xunit;

namespace KairosDbClient.IntegrationTests
{
    public class RestClientTests
    {
        private readonly RestClient _client;

        public RestClientTests()
        {
            _client = new RestClient("http://172.16.132.136:8083/");
        }
        
        [Fact]
        public async void RestClient_posts_metrics()
        {
            var metricName = GetUniqueMetricName();

            var dataPoint = new DataPoint(DateTime.UtcNow.MillisecondsSinceEpoch(), 5L);

            var metric = new Metric(metricName)
                .AddTag("route_id", "1")
                .AddDataPoint(dataPoint);

            await _client.AddMetricsAsync(new[] {metric});

            var query = new QueryBuilder()
                .SetStart(TimeSpan.FromSeconds(5))
                .AddQueryMetric(new QueryMetric(metricName));

            Thread.Sleep(TimeSpan.FromSeconds(2));

            var response = await _client.QueryMetricsAsync(query);

            response.Queries.Should().HaveCount(1);
            response.Queries[0].SampleSize.Should().Be(1);
            response.Queries[0].Results.Should().HaveCount(1);
            response.Queries[0].Results[0].DataPoints.Single().ShouldBeEquivalentTo(dataPoint);
        }

        [Fact]
        public async void QueryMetricsAsync_uses_sum_aggregator()
        {
            var metricName = GetUniqueMetricName();

            var time = DateTime.UtcNow.MillisecondsSinceEpoch();

            var dataPoint = new DataPoint(time, 10L);
            var dataPoint2 = new DataPoint(time + 1, 30L);

            var metric = new Metric(metricName)
                .AddTag("route_id", "1")
                .AddDataPoint(dataPoint2)
                .AddDataPoint(dataPoint);

            await _client.AddMetricsAsync(new[] { metric });

            var queryMetric = new QueryMetric(metricName)
                .AddAggregator(new SumAggregator(1, TimeUnit.Minutes));

            var query = new QueryBuilder()
                .SetStart(TimeSpan.FromSeconds(10))
                .AddQueryMetric(queryMetric);

            Thread.Sleep(TimeSpan.FromSeconds(2));

            var response = await _client.QueryMetricsAsync(query);

            response.Queries.Should().HaveCount(1);
            response.Queries[0].Results.Should().HaveCount(1);
            response.Queries[0].Results[0].DataPoints.Single().Value.Should().Be(40L);
        }

        [Fact]
        public async void QueryMetricsAsync_uses_avg_aggregator()
        {
            var metricName = GetUniqueMetricName();

            var time = DateTime.UtcNow.MillisecondsSinceEpoch();

            var dataPoint = new DataPoint(time, 10L);
            var dataPoint2 = new DataPoint(time + 1, 30L);

            var metric = new Metric(metricName)
                .AddTag("route_id", "1")
                .AddDataPoint(dataPoint2)
                .AddDataPoint(dataPoint);

            await _client.AddMetricsAsync(new[] { metric });

            var queryMetric = new QueryMetric(metricName)
                .AddAggregator(new AverageAggregator(1, TimeUnit.Minutes));

            var query = new QueryBuilder()
                .SetStart(TimeSpan.FromSeconds(10))
                .AddQueryMetric(queryMetric);

            Thread.Sleep(TimeSpan.FromSeconds(2));

            var response = await _client.QueryMetricsAsync(query);

            response.Queries.Should().HaveCount(1);
            response.Queries[0].Results.Should().HaveCount(1);
            response.Queries[0].Results[0].DataPoints.Single().Value.Should().Be(20L);
        }

        [Fact]
        public async void QueryMetricsAsync_uses_group_by_tag()
        {
            var metricName = GetUniqueMetricName();

            var dataPoint = new DataPoint(DateTime.UtcNow.MillisecondsSinceEpoch(), 10L);

            var metric = new Metric(metricName)
                .AddTag("route_id", "1")
                .AddDataPoint(dataPoint);

            var dataPoint2 = new DataPoint(DateTime.UtcNow.MillisecondsSinceEpoch(), 5L);

            var metric2 = new Metric(metricName)
                .AddTag("route_id", "2")
                .AddDataPoint(dataPoint2);

            await _client.AddMetricsAsync(new[] {metric, metric2});

            var queryMetric = new QueryMetric(metricName)
                .AddGroupBy(new GroupByTag("route_id"));

            var query = new QueryBuilder()
                .SetStart(TimeSpan.FromSeconds(5))
                .AddQueryMetric(queryMetric);

            Thread.Sleep(TimeSpan.FromSeconds(2));

            var response = await _client.QueryMetricsAsync(query);

            response.Queries.Single().SampleSize.Should().Be(2);

            response.Queries.Single().Results
                .Should()
                .HaveCount(2)
                .And
                .Contain(result => result.DataPoints.Single().LongValue == 10L)
                .And
                .Contain(result => result.DataPoints.Single().LongValue == 5L);
        }

        private string GetUniqueMetricName()
        {
            return Guid.NewGuid().ToString();
        }
    }
}