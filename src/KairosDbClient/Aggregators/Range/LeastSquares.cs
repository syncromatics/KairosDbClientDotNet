namespace KairosDbClient.Aggregators.Range
{
    public class LeastSquaresAggregator: RangeAggregator
    {
        public LeastSquaresAggregator(int value, TimeUnit unit) : base("least_squares", value, unit)
        {
            
        }
    }
}
