using System.Collections.Generic;

namespace ArdoqFluentModels.Azure.Model
{
    public class EventHub
    {
        public string Name { get; }
        public List<ConsumerGroup> ConsumerGroups { get; } = new List<ConsumerGroup>();

        public EventHub(string name)
        {
            Name = name;
        }

        public void AppendToFlattenedStructure(List<object> list)
        {
            list.Add(this);
            list.AddRange(ConsumerGroups);
        }
    }
}