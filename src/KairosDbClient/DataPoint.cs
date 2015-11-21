namespace KairosDbClient
{
    public class DataPoint
    {
        public long Timestamp { get; set; }
        public object Value { get; set; }

        public DataPoint()
        {

        }

        public DataPoint(long timestamp, object value)
        {
            Timestamp = timestamp;
            Value = value;
        }

        public double? DoubleValue => Value as double?;

        public long? LongValue => Value as long?;
    }
}
