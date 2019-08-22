using System.Collections.Generic;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace ArdoqFluentModels.Azure.Model
{
    public class SearchService : ResourceBase
    {
        public List<SearchServiceIndex> Indexes { get; } = new List<SearchServiceIndex>();

        public SearchService(IResource resource) : base(resource)
        {
        }

        public void AppendToFlattenedStructure(List<object> list)
        {
            list.Add(this);
            list.AddRange(Indexes);
        }
    }
}