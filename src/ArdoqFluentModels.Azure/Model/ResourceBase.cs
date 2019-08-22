using System.Collections.Generic;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace ArdoqFluentModels.Azure.Model
{
    public abstract class ResourceBase
    {
        public string Name { get; }
        public IReadOnlyDictionary<string, string> Tags;

        protected ResourceBase(IResource resource)
        {
            Name = resource.Name;
            Tags = resource.Tags;
        }

        public string GetTag(string key)
        {
            if (Tags == null || !Tags.ContainsKey(key))
            {
                return null;
            }

            return Tags[key];
        }
    }
}
