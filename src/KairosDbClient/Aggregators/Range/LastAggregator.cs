namespace KairosDbClient.Aggregators.Range
{
    public class LastAggregator : RangeAggregator
    {
        public LastAggregator(int value, TimeUnit unit) : base("last", value, unit)
        {
            
        }
    }
}