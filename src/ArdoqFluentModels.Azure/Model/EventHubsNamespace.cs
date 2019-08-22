using System.Collections.Generic;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace ArdoqFluentModels.Azure.Model
{
    public class EventHubsNamespace : ResourceBase
    {
        public List<EventHub> EventHubs { get; } = new List<EventHub>();

        public EventHubsNamespace(IResource resource) : base(resource)
        {
        }

        public void AppendToFlattenedStructure(List<object> list)
        {
            list.Add(this);

            foreach (var eh in EventHubs)
            {
                eh.AppendToFlattenedStructure(list);
            }
        }
    }
}
