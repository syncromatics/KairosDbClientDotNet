namespace KairosDbClient.Aggregators.Range
{
    public class SumAggregator : RangeAggregator
    {
        public SumAggregator(int value, TimeUnit unit) : base("sum", value, unit)
        {
            
        }
    }
}