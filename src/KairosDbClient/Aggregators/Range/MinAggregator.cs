namespace KairosDbClient.Aggregators.Range
{
    public class MinAggregator : RangeAggregator
    {
        public MinAggregator(int value, TimeUnit unit) : base("min", value, unit)
        {
            
        }
    }
}