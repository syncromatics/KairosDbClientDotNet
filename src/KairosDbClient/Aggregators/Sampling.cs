namespace KairosDbClient.Aggregators
{
    public class Sampling
    {
        public int Value { get; set; }
        public TimeUnit Unit { get; set; }

        public Sampling(int value, TimeUnit unit)
        {
            Value = value;
            Unit = unit;
        }
    }
}