using System.Collections.Generic;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.ServiceBus.Fluent;

namespace ArdoqFluentModels.Azure.Model
{
    public class ServiceBusNamespace : ResourceBase
    {
        private readonly List<ServiceBusQueue> _queues = new List<ServiceBusQueue>();
        private readonly List<ServiceBusTopic> _topics = new List<ServiceBusTopic>();

        public IEnumerable<ServiceBusQueue> Queues => _queues;

        public IEnumerable<ServiceBusTopic> Topics => _topics;

        public string EndpointUrl { get; private set; }

        public ServiceBusNamespace(IResource resource) : base(resource)
        {
            EndpointUrl = $"sb://{resource.Name}.servicebus.windows.net/";
        }

        public void AddQueue(ServiceBusQueue queue)
        {
            _queues.Add(queue);
        }

        public void AddTopic(ServiceBusTopic topic)
        {
            _topics.Add(topic);
        }

        public void AppendToFlattenedStructure(List<object> list)
        {
            list.Add(this);
            list.AddRange(Queues);
            list.AddRange(Topics);
        }
    }
}