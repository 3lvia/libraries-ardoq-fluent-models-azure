using ArdoqFluentModels.Search;
using ArdoqFluentModels.Azure.ConnectionStrings;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ArdoqFluentModels.Azure.Tests.ConnectionStrings
{
    public class ConnectionStringExtensionsTests
    {
        [Fact]
        public void GetConnectionStringType_RandomString_IsNotConnectionString()
        {
            // Arrange
            var randomString = "this-string-is-not-a-connectionstring";

            // Act
            var connectionStringType = randomString.GetConnectionStringType();

            // Assert
            Assert.Equal(ConnectionStringType.NotConnectionString, connectionStringType);
        }

        [Fact]
        public void GetConnectionStringType_ForServiceBus_ServiceBusType()
        {
            // Arrange
            var connectionString = "Endpoint=sb://my-sb-namespace.servicebus.windows.net/;SharedAccessKeyName=send_listen;SharedAccessKey=sadfsdfsdffnsadBHb798789asdnakjsndlkasdcx000=;EntityPath=entpath";

            // Act
            var connectionStringType = connectionString.GetConnectionStringType();

            // Assert
            Assert.Equal(ConnectionStringType.ServiceBusOrEventHub, connectionStringType);
        }

        [Fact]
        public void GetConnectionStringType_ForSql_SqlType()
        {
            // Arrange
            var connectionString = "Server=tcp:mdm-sqlserver.database.windows.net,1433;Initial Catalog=abc-def;Persist Security Info=False;User ID=admin;Password=Pa$$wrd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            // Act
            var connectionStringType = connectionString.GetConnectionStringType();

            // Assert
            Assert.Equal(ConnectionStringType.Sql, connectionStringType);
        }

        [Fact]
        public void GetConnectionStringType_ForStorage_StorageType()
        {
            // Arrange
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=mystorageaccount;AccountKey=jasdf3475NINI7sdf7fhasidcvnYHYHdfs0==;EndpointSuffix=core.windows.net";

            // Act
            var connectionStringType = connectionString.GetConnectionStringType();

            // Assert
            Assert.Equal(ConnectionStringType.Storage, connectionStringType);
        }

        [Fact]
        public void GetConnectionStringType_ForCosmosDb_CosmosDbType()
        {
            // Arrange
            var connectionString = "AccountEndpoint=https://mydocdb.documents.azure.com:443/;AccountKey=fhwefwuenfduiwendwednwednweuidnwuiednweidnewdi==;";

            // Act
            var connectionStringType = connectionString.GetConnectionStringType();

            // Assert
            Assert.Equal(ConnectionStringType.CosmosDb, connectionStringType);
        }

        [Fact]
        public void ToSearchSpecFromConnectionString_NotConnectionString_ThrowsArgumentException()
        {
            // Arrange
            var connectionStrng = "not-a-connection-string";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => connectionStrng.ToSearchSpecFromConnectionString(""));
            Assert.Equal("Not a connection string.", ex.Message);
        }

        [Fact]
        public void GetSearchSpecForServiceBusOrEventHubNamespace_ServiceBusNamespace_ReturnsExpectedSearchSpec()
        {
            // Arrange
            var connectionString = "Endpoint=sb://my-sb-namespace.servicebus.windows.net/;SharedAccessKeyName=send_listen;SharedAccessKey=sadfsdfsdffnsadBHb798789asdnakjsndlkasdcx000=;EntityPath=entpath";

            // Act
            var searchSpec = connectionString.ToSearchSpecFromConnectionString("");

            // Assert
            Assert.Equal(2, searchSpec.Elements.Count);

            var element = (ComponentTypeAndFieldSearchSpecElement)searchSpec.Elements[0];
            Assert.Equal("ServiceBusNamespace", element.ComponentType);
            Assert.Equal("sb://my-sb-namespace.servicebus.windows.net/", element.FieldFilters["uri"]);

            element = (ComponentTypeAndFieldSearchSpecElement)searchSpec.Elements[1];
            Assert.Equal("entpath", element.Name);
        }
    }
}
