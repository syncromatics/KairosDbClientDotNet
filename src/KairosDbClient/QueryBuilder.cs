using System;
using System.Collections.Generic;
using KairosDbClient.Aggregators;
using KairosDbClient.Extensions;
using Newtonsoft.Json;

namespace KairosDbClient
{
    public class QueryBuilder
    {
        [JsonIgnore]
        public DateTimeOffset? AbsoluteStart { get; private set; }
        [JsonIgnore]
        public DateTimeOffset? AbsoluteEnd { get; private set; }
        [JsonProperty("cache_time")]
        public TimeSpan? CacheTime { get; private set; }
        [JsonProperty("time_zone")]
        public string TimeZone { get; private set; }

        private readonly List<QueryMetric> _queryMetrics = new List<QueryMetric>();

        [JsonProperty("start_absolute")]
        public long? StartAbsolute => AbsoluteStart?.DateTime.MillisecondsSinceEpoch();

        [JsonProperty("end_absolute")]
        public long? EndAbsolute => AbsoluteStart?.DateTime.MillisecondsSinceEpoch();

        [JsonProperty("start_relative")]
        public RelativeTime StartRelative { get; private set; }
        [JsonProperty("end_relative")]
        public RelativeTime EndRelative { get; private set; }

        public IReadOnlyList<QueryMetric> Metrics => _queryMetrics;  

        public QueryBuilder SetStart(TimeSpan start)
        {
            if (AbsoluteStart.HasValue)
            {
                throw new ArgumentException("Both relative and absolute start times cannot be set.");
            }
            StartRelative = new RelativeTime((long)start.TotalMilliseconds, TimeUnit.Milliseconds);
            return this;
        }

        public QueryBuilder SetEnd(TimeSpan end)
        {
            if (AbsoluteEnd.HasValue)
            {
                throw new ArgumentException("Both relative and absolute end times cannot be set.");
            }
            EndRelative = new RelativeTime((long)end.TotalMilliseconds, TimeUnit.Milliseconds);
            return this;
        }

        public QueryBuilder SetStart(DateTimeOffset start)
        {
            if (StartRelative != null)
            {
                throw new ArgumentException("Both relative and absolute start times cannot be set.");
            }
            AbsoluteStart = start;
            return this;
        }

        public QueryBuilder SetEnd(DateTimeOffset end)
        {
            if (StartRelative != null)
            {
                throw new ArgumentException("Both relative and absolute end times cannot be set.");
            }
            AbsoluteEnd = end;
            return this;
        }

        public QueryBuilder SetStart(RelativeTime start)
        {
            if (AbsoluteStart.HasValue)
            {
                throw new ArgumentException("Both relative and absolute start times cannot be set.");
            }
            StartRelative = start;
            return this;
        }

        public QueryBuilder SetEnd(RelativeTime end)
        {
            if (AbsoluteEnd.HasValue)
            {
                throw new ArgumentException("Both relative and absolute end times cannot be set.");
            }
            EndRelative = end;
            return this;
        }

        public QueryBuilder SetCacheTime(TimeSpan cacheTime)
        {
            CacheTime = cacheTime;
            return this;
        }

        public QueryBuilder SetTimeZone(string timezone)
        {
            TimeZone = timezone;
            return this;
        }

        public QueryBuilder AddQueryMetric(QueryMetric metric)
        {
            _queryMetrics.Add(metric);
            return this;
        }
    }
}
