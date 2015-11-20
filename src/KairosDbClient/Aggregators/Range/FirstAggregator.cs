namespace KairosDbClient.Aggregators.Range
{
    public class FirstAggregator : RangeAggregator
    {
        public FirstAggregator(int value, TimeUnit unit) : base("first", value, unit)
        {
            
        }
    }
}