using System;
using System.Collections.Generic;
using System.Text;

namespace ArdoqFluentModels.Azure
{
    public class AzureSourceModelProvider : ISourceModelProvider
    {
        private readonly IAzureReader _reader;

        public AzureSourceModelProvider(IAzureReader reader)
        {
            _reader = reader;
        }

        public IEnumerable<object> GetSourceModel()
        {
            var subscription = _reader.ReadSubscription();

            return subscription?.ToFlattenedStructure();
        }
    }
}
