namespace KairosDbClient.Aggregators.Range
{
    public class CountAggregator : RangeAggregator
    {
        public CountAggregator(int value, TimeUnit unit) : base("count", value, unit)
        {
            
        }
    }
}