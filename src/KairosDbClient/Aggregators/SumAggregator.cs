using KairosDbClient.Aggregators.Range;

namespace KairosDbClient.Aggregators
{
    public class SumAggregator : RangeAggregator
    {
        public SumAggregator(int value, TimeUnit unit) : base("sum", value, unit)
        {
            
        }
    }
}