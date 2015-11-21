namespace KairosDbClient.Aggregators
{
    /// <summary>
    /// Base aggregator class. Extend this for your custom aggregations
    /// </summary>
    public class Aggregator
    {
        public string Name { get; protected set; }
    }
}
