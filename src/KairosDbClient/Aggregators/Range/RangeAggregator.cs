namespace KairosDbClient.Aggregators.Range
{
    public class RangeAggregator : Aggregator
    {
        public RelativeTime Sampling { get; private set; }

        internal RangeAggregator(string name, int value, TimeUnit timeUnit)
        {
            Name = name;
            Sampling = new RelativeTime(value, timeUnit);
        }
    }
}
