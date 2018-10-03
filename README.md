# KairosDbClientDotNet

This is a .NET library to send metrics to and receive metrics from KairosDB.

## Quickstart

### Sending Metrics
```c#
    Metric metric = new Metric("metric_name")
        .AddTag("server", "1")
        .AddDataPoint(new DataPoint(DateTime.UtcNow.MillisecondsSinceEpoch(), 5L);
        
    RestClient client = new RestClient("http://localhost:8083");
    await client.AddMetricsAsync(new [] {metric});
```
    
### Querying DataPoints
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

## Building



[![Travis](https://img.shields.io/travis/syncromatics/KairosDbClientDotNet.svg)](https://travis-ci.org/syncromatics/KairosDbClientDotNet)
[![NuGet](https://img.shields.io/nuget/v/KairosDbClient.svg)](https://www.nuget.org/packages/KairosDbClient/)
[![NuGet Pre Release](https://img.shields.io/nuget/vpre/KairosDbClient.svg)](https://www.nuget.org/packages/KairosDbClient/)

## Code of Conduct

We are committed to fostering an open and welcoming environment. Please read our [code of conduct](CODE_OF_CONDUCT.md) before participating in or contributing to this project.

## Contributing

We welcome contributions and collaboration on this project. Please read our [contributor's guide](CONTRIBUTING.md) to understand how best to work with us.

## License and Authors

[![GMV Syncromatics Engineering logo](https://secure.gravatar.com/avatar/645145afc5c0bc24ba24c3d86228ad39?size=16) GMV Syncromatics Engineering](https://github.com/syncromatics)

[![license](https://img.shields.io/github/license/syncromatics/KairosDbClientDotNet.svg)](https://github.com/syncromatics/KairosDbClientDotNet/blob/master/LICENSE)
[![GitHub contributors](https://img.shields.io/github/contributors/syncromatics/KairosDbClientDotNet.svg)](https://github.com/syncromatics/KairosDbClientDotNet/graphs/contributors)

This software is made available by GMV Syncromatics Engineering under the MIT license.