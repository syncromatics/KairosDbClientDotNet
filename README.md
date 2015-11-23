# KairosDbClientDotNet

A dotnet kairosdb client.

## Sending Metrics
```c#
    Metric metric = new Metric("metric_name")
        .AddTag("server", "1")
        .AddDataPoint(new DataPoint(DateTime.UtcNow.MillisecondsSinceEpoch(), 5L);
        
    RestClient client = new RestClient("http://localhost:8083");
    await client.AddMetricsAsync(new [] {metric});
```
    
## Querying DataPoints
```c#
    QueryMetric queryMetric = new QueryMetric("metric_name")
        .AddAggregator(new SumAggregator(1, TimeUnit.Minutes)
        .AddGroupBy(new GroupByTag("server"));
        
    QueryBuilder query = new QueryBuilder()
        .SetStart(TimeSpan.FromSeconds(5))
        .AddQueryMetric(queryMetric);
        
    RestClient client = new RestClient("http://localhost:8083");
    QueryResponse response = await client.QueryMetricsAsync(query);
```