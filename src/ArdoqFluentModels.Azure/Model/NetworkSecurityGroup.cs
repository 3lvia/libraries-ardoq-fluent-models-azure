using System.Collections.Generic;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace ArdoqFluentModels.Azure.Model
{
    public class NetworkSecurityGroup : ResourceBase
    {
        public List<NetworkInterface> NetworkInterfaces { get; } = new List<NetworkInterface>();

        public NetworkSecurityGroup(IResource resource) : base(resource)
        {
        }

        public void AppendToFlattenedStructure(List<object> list)
        {
            list.Add(this);
            list.AddRange(NetworkInterfaces);
        }
    }
}
