namespace KairosDbClient.Aggregators.Range
{
    public class AverageAggregator : RangeAggregator
    {
        public AverageAggregator(int value, TimeUnit unit) : base("avg", value, unit)
        {
            
        }
    }
}