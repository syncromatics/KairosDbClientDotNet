using System;
using System.Collections.Generic;
using KairosDbClient.Aggregators;
using KairosDbClient.Extensions;
using Newtonsoft.Json;

namespace KairosDbClient
{
    /// <summary>
    /// Use to query 1 or more metrics for a time range. 
    /// A start time is required, either absolute or relative.
    /// End time is optional and defaults to now
    /// </summary>
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
        public long? StartAbsolute => AbsoluteStart?.UtcDateTime.MillisecondsSinceEpoch();

        [JsonProperty("end_absolute")]
        public long? EndAbsolute => AbsoluteEnd?.UtcDateTime.MillisecondsSinceEpoch();

        [JsonProperty("start_relative")]
        public RelativeTime StartRelative { get; private set; }
        [JsonProperty("end_relative")]
        public RelativeTime EndRelative { get; private set; }

        public IReadOnlyList<QueryMetric> Metrics => _queryMetrics;  

        /// <summary>
        /// This will be passed to kairosdb as a relative milliseconds value
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public QueryBuilder SetStart(TimeSpan start)
        {
            if (AbsoluteStart.HasValue)
            {
                throw new ArgumentException("Both relative and absolute start times cannot be set.");
            }
            StartRelative = new RelativeTime((long)start.TotalMilliseconds, TimeUnit.Milliseconds);
            return this;
        }

        /// <summary>
        /// This will be passed to kairosdb as a relative milliseconds value
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
        public QueryBuilder SetEnd(TimeSpan end)
        {
            if (AbsoluteEnd.HasValue)
            {
                throw new ArgumentException("Both relative and absolute end times cannot be set.");
            }
            EndRelative = new RelativeTime((long)end.TotalMilliseconds, TimeUnit.Milliseconds);
            return this;
        }

        /// <summary>
        /// This will be passed to kairosdb as an absolute time
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public QueryBuilder SetStart(DateTimeOffset start)
        {
            if (StartRelative != null)
            {
                throw new ArgumentException("Both relative and absolute start times cannot be set.");
            }
            AbsoluteStart = start;
            return this;
        }

        /// <summary>
        /// This will be passed to kairosdb as an absolute time
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
        public QueryBuilder SetEnd(DateTimeOffset end)
        {
            if (StartRelative != null)
            {
                throw new ArgumentException("Both relative and absolute end times cannot be set.");
            }
            AbsoluteEnd = end;
            return this;
        }

        /// <summary>
        /// This will be passed to kairosdb as a relative time with the unit specified
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public QueryBuilder SetStart(RelativeTime start)
        {
            if (AbsoluteStart.HasValue)
            {
                throw new ArgumentException("Both relative and absolute start times cannot be set.");
            }
            StartRelative = start;
            return this;
        }

        /// <summary>
        /// This will be passed to kairosdb as a relative time with the unit specified
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Add a metric to query for
        /// </summary>
        /// <param name="metric"></param>
        /// <returns></returns>
        public QueryBuilder AddQueryMetric(QueryMetric metric)
        {
            _queryMetrics.Add(metric);
            return this;
        }
    }
}
