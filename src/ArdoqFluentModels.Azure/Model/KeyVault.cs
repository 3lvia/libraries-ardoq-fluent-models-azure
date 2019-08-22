using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace ArdoqFluentModels.Azure.Model
{
    public class KeyVault : ResourceBase
    {

        public KeyVault(IResource resource) : base(resource) { }

        public string Uri { get; set; }
    }
}
