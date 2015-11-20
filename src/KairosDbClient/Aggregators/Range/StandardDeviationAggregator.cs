namespace KairosDbClient.Aggregators.Range
{
    public class StandardDeviationAggregator : RangeAggregator
    {
        public StandardDeviationAggregator(int value, TimeUnit unit) : base("dev", value, unit)
        {
            
        }
    }
}