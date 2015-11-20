namespace KairosDbClient.Aggregators
{
    public class RateAggregator : Aggregator
    {
        public TimeUnit Unit { get; private set; }

        public RateAggregator(TimeUnit unit)
        {
            Unit = unit;
            Name = "rate";
        }
    }
}