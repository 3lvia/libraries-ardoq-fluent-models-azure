using ArdoqFluentModels.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArdoqFluentModels.Azure.ConnectionStrings
{
    public static class ConnectionStringExtensions
    {
        public static ConnectionStringType GetConnectionStringType(this string connectionString)
        {
            if (connectionString.IsAzureStorageConnectionString())
            {
                return ConnectionStringType.Storage;
            }

            if (connectionString.IsSqlServerConnectionString())
            {
                return ConnectionStringType.Sql;
            }

            if (connectionString.IsAzureServiceBusConnectionString())
            {
                return ConnectionStringType.ServiceBusOrEventHub;
            }

            if (connectionString.IsCosmosDbConnectionString())
            {
                return ConnectionStringType.CosmosDb;
            }

            return ConnectionStringType.NotConnectionString;
        }

        public static SearchSpec ToSearchSpecFromConnectionString(this string connectionString, string searchFolder)
        {
            var connectionStringType = connectionString.GetConnectionStringType();
            if (ConnectionStringType.NotConnectionString == connectionStringType)
            {
                throw new ArgumentException("Not a connection string.");
            }

            SearchSpec res = null;
            switch (connectionStringType)
            {
                case ConnectionStringType.ServiceBusOrEventHub:
                    res = GetSearchSpecForServiceBusOrEventHubNamespace(connectionString, searchFolder);
                    break;
            }

            return res;
        }

        private static SearchSpec GetSearchSpecForServiceBusOrEventHubNamespace(string connectionString, string searchFolder)
        {
            var arr = connectionString.Split(';');
            var endpoint = arr.Single(a => a.StartsWith("Endpoint="));
            var entityPath = arr.Single(a => a.StartsWith("EntityPath="));

            var spec = new SearchSpec(searchFolder);

            var namespaceElement = new ComponentTypeAndFieldSearchSpecElement();
            namespaceElement.ComponentType = "ServiceBusNamespace";
            namespaceElement.AddFieldFilter("uri", endpoint.Split('=')[1]);
            spec.AddElement(namespaceElement);

            var entityElement = new ComponentTypeAndFieldSearchSpecElement();
            entityElement.Name = entityPath.Split('=')[1];
            spec.AddElement(entityElement);

            return spec;
        }

        private static bool IsAzureStorageConnectionString(this string connectionString)
        {
            return connectionString.Contains("DefaultEndpointsProtocol=https;") &&
                   connectionString.Contains("AccountName=");
        }

        private static bool IsAzureServiceBusConnectionString(this string connectionString)
        {
            return connectionString.Contains("servicebus.windows.net")
                   && connectionString.Contains("EntityPath=");
        }

        private static bool IsSqlServerConnectionString(this string connectionString)
        {
            return connectionString.Contains("Initial Catalog=");
        }

        private static bool IsCosmosDbConnectionString(this string connectionString)
        {
            return connectionString.Contains("AccountEndpoint=")
                   && connectionString.Contains("AccountKey=");
        }
    }
}
