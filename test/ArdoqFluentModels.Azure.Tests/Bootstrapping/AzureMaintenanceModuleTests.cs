using ArdoqFluentModels.Azure.Bootstrapping;
using Xunit;

namespace ArdoqFluentModels.Azure.Tests.Bootstrapping
{
    public class AzureMaintenanceModuleTests
    {
        [Fact]
        public void Configure_HappyDays_NoExceptions()
        {
            // Arrange
            var builder = new ArdoqModelMappingBuilder(null, null, null);
            var module = new AzureMaintenanceModule();

            // Act
            module.Configure(builder);
        }
    }
}
