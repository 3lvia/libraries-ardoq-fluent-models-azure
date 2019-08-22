using System.Collections.Generic;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace ArdoqFluentModels.Azure.Model
{
    public class SqlServer : ResourceBase
    {
        public List<SqlDatabase> Databases { get; } = new List<SqlDatabase>();

        public SqlServer(IResource resource) : base(resource)
        {
        }

        public void AppendToFlattenedStructure(List<object> list)
        {
            list.Add(this);
            list.AddRange(Databases);
        }
    }
}