Azure Source Model Provider for Ardoq Fluent Models
===================================================

This library provides an implementation of ArdoqFluentModels.ISourceModelProvider that is able to provide am interconnected model of Azure resources. This data can then be used to document your Azure infrastructure in Ardoq.

## Usage
The typical use case would be to implement a .NET Core console application that hosts the documentation job. The sample below shows how you could bootstrap and run Ardoq Fluent Models with an Azure provider once:

```csharp
var azureClientId = "GET ClientId FROM CONFIG";
var azureClientSecret = "GET Secret FROM CONFIG";
var azureTenantId = "GET TenantID FROM CONFIG";
var subscriptionId = "GET SUbscriptionId FROM CONFIG";

var reader = new AzureReader(
                azureClientId,
                azureClientSecret,
                azureTenantId,
                subscriptionId);

var provider = new AzureSourceModelProvider(reader);

 var builder =
                    new ArdoqModelMappingBuilder("<Ardoq URL from config>", "<Ardoq token from config>", "<Ardoq organization from config>")
                        .WithWorkspaceNamed("My workspace")
                        .WithFolderNamed("A folder in Ardoq")
                        .WithTemplate("Ardoq template name");

var module = new AzureMaintenanceModule();
module.Configure(builder);

var session = builder.Build();
session.Run(provider).Wait();
```