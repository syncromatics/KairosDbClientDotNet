using System.Collections.Generic;

namespace KairosDbClient.Grouping
{
    public class GroupByTag : GroupBy
    {
        private readonly List<string> _tags = new List<string>(); 
        public IReadOnlyList<string> Tags => _tags;

        public GroupByTag() : base("tag")
        {

        }

        public GroupByTag(params string[] tags) : base("tag")
        {
            _tags.AddRange(tags);
        }

        public GroupByTag AddTag(string tag)
        {
            _tags.Add(tag);
            return this;
        }

        public GroupByTag AddTags(IEnumerable<string> tags)
        {
            _tags.AddRange(tags);
            return this;
        }
    }
}