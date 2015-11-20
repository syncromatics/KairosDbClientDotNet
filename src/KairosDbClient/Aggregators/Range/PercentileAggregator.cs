namespace KairosDbClient.Aggregators.Range
{
    public class PercentileAggregator : RangeAggregator
    {
        public double Percentile { get; private set; }

        public PercentileAggregator(int value, TimeUnit unit, double percentile) : base ("percentile", value, unit)
        {
            Percentile = percentile;
        }
    }
}