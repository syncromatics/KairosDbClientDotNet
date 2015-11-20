namespace KairosDbClient.Aggregators.Range
{
    public class MaxAggregator : RangeAggregator
    {
        public MaxAggregator(int value, TimeUnit unit) : base("max", value, unit)
        {
            
        }
    }
}