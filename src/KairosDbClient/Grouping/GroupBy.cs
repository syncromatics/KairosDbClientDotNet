namespace KairosDbClient.Grouping
{
    public abstract class GroupBy
    {
        public string Name { get; protected set; }

        protected GroupBy(string name)
        {
            Name = name;
        }
    }
}
