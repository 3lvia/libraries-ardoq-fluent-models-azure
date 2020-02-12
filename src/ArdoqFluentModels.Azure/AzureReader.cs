using ArdoqFluentModels.Azure.Model;
using Microsoft.Azure.Management.Compute.Fluent;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArdoqFluentModels.Azure
{
    public class AzureReader : IAzureReader
    {
        private readonly IAzure _azure;
        private readonly ILogger _logger;

        public string SubscriptionName { get; }

        public AzureReader(
            string clientId,
            string clientSecret,
            string tenantId,
            string subscriptionId,
            ILogger logger = null)
        {
            _logger = logger ?? new ConsoleLogger();

            var credentials = SdkContext.AzureCredentialsFactory
                .FromServicePrincipal(
                    clientId,
                    clientSecret,
                    tenantId,
                    AzureEnvironment.AzureGlobalCloud);

            _azure = Microsoft.Azure.Management.Fluent.Azure
                .Configure()
                .Authenticate(credentials)
                .WithSubscription(subscriptionId);

            SubscriptionName = _azure.GetCurrentSubscription().DisplayName;
        }

        public Subscription ReadSubscription()
        {
            Subscription subscription = new Subscription(SubscriptionName);
            subscription.ResourceGroups.AddRange(ReadResourceGroups());

            return subscription;
        }

        public List<ResourceGroup> ReadResourceGroups()
        {
            var resourceGroups = new List<ResourceGroup>();

            var azureResourceGroups = _azure.ResourceGroups.List();

            //TODO add filtering to only select relevant Resource Groups

            foreach (var azureResourceGroup in azureResourceGroups)
            {
                var resourceGroup = new ResourceGroup(azureResourceGroup);

                try 
                { 
                    resourceGroup.EventHubsNamespaces.AddRange(ReadEventHubsNamespaces(azureResourceGroup));
                    resourceGroup.SqlServers.AddRange(ReadSqlDatabases(azureResourceGroup));
                    resourceGroup.StorageAccounts.AddRange(ReadStorageAccounts(azureResourceGroup));
                    resourceGroup.ServiceBusNamespaces.AddRange(ReadServiceBusNamespaces(azureResourceGroup));
                    resourceGroup.SearchServices.AddRange(ReadSearchServices(azureResourceGroup));
                    resourceGroup.RedisCaches.AddRange(ReadRedisCaches(azureResourceGroup));
                    resourceGroup.CosmosDBAccounts.AddRange(ReadCosmosDBAccounts(azureResourceGroup));
                    resourceGroup.KeyVaults.AddRange(ReadKeyVaults(azureResourceGroup));

                    // resourceGroup.AlertRules.AddRange(ReadAlertRules(azureResourceGroup));
                    // resourceGroup.AccessManagements.AddRange(ReadAccessManagements(azureResourceGroup));
                    resourceGroup.ApplicationGateways.AddRange(ReadApplicationGateways(azureResourceGroup));
                    resourceGroup.AutoscaleSettings.AddRange(ReadAutoscaleSettings(azureResourceGroup));
                    resourceGroup.AvailabilitySets.AddRange(ReadAvailabilitySets(azureResourceGroup));
                    resourceGroup.BatchAccounts.AddRange(ReadBatchAccounts(azureResourceGroup));
                    resourceGroup.CdnProfiles.AddRange(ReadCdnProfiles(azureResourceGroup));
                    // resourceGroup.ComputeSkus.AddRange(ReadComputeSkus(azureResourceGroup));
                    resourceGroup.Disks.AddRange(ReadDisks(azureResourceGroup));
                    //resourceGroup.DnsZones.AddRange(ReadDnsZones(azureResourceGroup));
                    resourceGroup.NetworkSecurityGroups.AddRange(ReadNetworkSecurityGroups(azureResourceGroup));
                    resourceGroup.VirtualNetworks.AddRange(ReadVirtualNetworks(azureResourceGroup));
                    resourceGroup.PublicIPAddresses.AddRange(ReadPublicIPAddresses(azureResourceGroup));
                    resourceGroup.Snapshots.AddRange(ReadSnapshots(azureResourceGroup));
                    resourceGroup.TrafficManagerProfiles.AddRange(ReadTrafficManagerProfiles(azureResourceGroup));
                    resourceGroup.VirtualMachines.AddRange(ReadVirtualMachines(azureResourceGroup));
                    resourceGroup.VirtualMachineCustomImages.AddRange(ReadVirtualMachineCustomImages(azureResourceGroup));
                    resourceGroup.VirtualMachineScaleSets.AddRange(ReadVirtualMachineScaleSets(azureResourceGroup));
                    resourceGroup.LoadBalancers.AddRange(ReadLoadBalancers(azureResourceGroup));

                    //ListApplicationInsights(resourceGroup);
                }
                catch (Exception ex) 
                { 
                    _logger.LogException(ex); 
                };

                resourceGroups.Add(resourceGroup);
            }
            return resourceGroups;
        }

        private void ListApplicationInsights(IResourceGroup resourceGroup)
        {
            //Is this possible through the API?
            //https://github.com/Azure/azure-libraries-for-net/issues/27
        }

        private IEnumerable<AlertRule> ReadAlertRules(IResourceGroup resourceGroup)
        {
            var alertRule = new AlertRule(resourceGroup);
            var alertRules = new List<AlertRule> { alertRule };
            var azureAlertRules = _azure.AlertRules;

            foreach (var metricAlert in azureAlertRules.MetricAlerts.ListByResourceGroup(resourceGroup.Name))
            {
                MetricAlert metric = new MetricAlert(metricAlert.Name);
                alertRule.MetricAlerts.Add(metric);
            }

            // Microsoft.Azure.Management.Monitor.Fluent.Models.ErrorResponseException. v
            // Operation returned an invalid status code 'NotFound'
            foreach (var activityLogAlert in azureAlertRules.ActivityLogAlerts.ListByResourceGroup(resourceGroup.Name))
            {
                ActivityLogAlert activity = new ActivityLogAlert(activityLogAlert.Name);
                alertRule.ActivityLogAlerts.Add(activity);
            }

            return alertRules;
        }

        private IEnumerable<AccessManagement> ReadAccessManagements(IResourceGroup resourceGroup)
        {
            var accessManagement = new AccessManagement(resourceGroup);
            var accessManagements = new List<AccessManagement> { accessManagement };
            var azureAccessManagement = _azure.AccessManagement;

            // Microsoft.Azure.Management.Graph.RBAC.Fluent.Models.GraphErrorException
            // Operation returned an invalid status code 'Forbidden'
            foreach (var activeDirectoryGroup in azureAccessManagement.ActiveDirectoryGroups.List())
            {
                var group = new ActiveDirectoryGroup(activeDirectoryGroup.Name);
                accessManagement.ActiveDirectoryGroups.Add(group);
            }

            foreach (var activeDirectoryApplication in azureAccessManagement.ActiveDirectoryApplications.List())
            {
                var application = new ActiveDirectoryApplication(activeDirectoryApplication.Name);
                accessManagement.ActiveDirectoryApplications.Add(application);
            }

            foreach (var activeDirectoryUser in azureAccessManagement.ActiveDirectoryUsers.List())
            {
                var user = new ActiveDirectoryUser(activeDirectoryUser.Name);
                accessManagement.ActiveDirectoryUsers.Add(user);
            }

            ////foreach (var roleAssignment in azureAccessManagement.RoleAssignments.ListByScope())
            ////{
            ////    var assignment = new RoleAssignment(roleAssignment.Name);
            ////    accessManagement.RoleAssignments.Add(assignment);
            ////}

            ////foreach (var roleDefinition in azureAccessManagement.RoleDefinitions.ListByScope())
            ////{
            ////    var definition = new ActiveDirectoryUser(roleDefinition.Name);
            ////    accessManagement.RoleDefinitions.Add(definition);
            ////}

            foreach (var servicePrincipal in azureAccessManagement.ServicePrincipals.List())
            {
                var principal = new ServicePrincipal(servicePrincipal.Name);
                accessManagement.ServicePrincipals.Add(principal);
            }

            return accessManagements;
        }

        private IEnumerable<ApplicationGateway> ReadApplicationGateways(IResourceGroup resourceGroup)
        {
            return _azure.ApplicationGateways.ListByResourceGroup(resourceGroup.Name)
                .Select(g => new ApplicationGateway(g));
        }

        private IEnumerable<AutoscaleSetting> ReadAutoscaleSettings(IResourceGroup resourceGroup)
        {
            return _azure.AutoscaleSettings.ListByResourceGroup(resourceGroup.Name)
                .Select(scale => new AutoscaleSetting(scale));
        }

        private IEnumerable<AvailabilitySet> ReadAvailabilitySets(IResourceGroup resourceGroup)
        {
            return _azure.AvailabilitySets.ListByResourceGroup(resourceGroup.Name)
                .Select(a => new AvailabilitySet(a));
        }

        private IEnumerable<BatchAccount> ReadBatchAccounts(IResourceGroup resourceGroup)
        {
            return _azure.BatchAccounts.ListByResourceGroup(resourceGroup.Name)
                .Select(a => new BatchAccount(a));
        }

        private IEnumerable<ComputeSku> ReadComputeSkus(IResourceGroup resourceGroup)
        {
            List<ComputeSku> computeSkus = new List<ComputeSku>();
            computeSkus.AddRange(_azure.ComputeSkus.ListByResourceType(ComputeResourceType.AvailabilitySets)
                .Select(s => new ComputeSku(resourceGroup))
                .ToList());
            computeSkus.AddRange(_azure.ComputeSkus.ListByResourceType(ComputeResourceType.Disks)
                .Select(s => new ComputeSku(resourceGroup))
                .ToList());
            computeSkus.AddRange(_azure.ComputeSkus.ListByResourceType(ComputeResourceType.Snapshots)
                .Select(s => new ComputeSku(resourceGroup))
                .ToList());
            computeSkus.AddRange(_azure.ComputeSkus.ListByResourceType(ComputeResourceType.VirtualMachines)
                .Select(s => new ComputeSku(resourceGroup))
                .ToList());

            return computeSkus;
        }

        private IEnumerable<CdnProfile> ReadCdnProfiles(IResourceGroup resourceGroup)
        {
            return _azure.CdnProfiles.ListByResourceGroup(resourceGroup.Name)
                .Select(a => new CdnProfile(a));
        }

        private IEnumerable<Disk> ReadDisks(IResourceGroup resourceGroup)
        {
            return _azure.Disks.ListByResourceGroup(resourceGroup.Name)
                .Select(d => new Disk(d));
        }

        private IEnumerable<DnsZone> ReadDnsZones(IResourceGroup resourceGroup)
        {
            return _azure.DnsZones.ListByResourceGroup(resourceGroup.Name)
                .Select(d => new DnsZone(d));
        }

        private IEnumerable<NetworkSecurityGroup> ReadNetworkSecurityGroups(IResourceGroup resourceGroup)
        {
            var networkSecurityGroups = new List<NetworkSecurityGroup>();

            var azurenetworkSecurityGroups = _azure.NetworkSecurityGroups.ListByResourceGroup(resourceGroup.Name);

            foreach (var securityGroup in azurenetworkSecurityGroups)
            {
                NetworkSecurityGroup group = new NetworkSecurityGroup(securityGroup);
                foreach (var networkInterfaceId in securityGroup.NetworkInterfaceIds)
                {
                    var networkInterface = _azure.NetworkInterfaces.GetById(networkInterfaceId);
                    if (networkInterface != null)
                    {
                        group.NetworkInterfaces.Add(new NetworkInterface(networkInterface));
                    }
                }
            }

            return networkSecurityGroups;
        }

        private IEnumerable<VirtualNetwork> ReadVirtualNetworks(IResourceGroup resourceGroup)
        {
            return _azure.Networks.ListByResourceGroup(resourceGroup.Name)
                .Select(n => new VirtualNetwork(n));
        }

        private IEnumerable<PublicIPAddress> ReadPublicIPAddresses(IResourceGroup resourceGroup)
        {
            return _azure.PublicIPAddresses.ListByResourceGroup(resourceGroup.Name)
                .Select(p => new PublicIPAddress(p));
        }

        private IEnumerable<Snapshot> ReadSnapshots(IResourceGroup resourceGroup)
        {
            return _azure.Snapshots.ListByResourceGroup(resourceGroup.Name)
                .Select(s => new Snapshot(s));
        }

        private IEnumerable<TrafficManagerProfile> ReadTrafficManagerProfiles(IResourceGroup resourceGroup)
        {
            return _azure.TrafficManagerProfiles.ListByResourceGroup(resourceGroup.Name)
                .Select(t => new TrafficManagerProfile(t));
        }

        private IEnumerable<VirtualMachine> ReadVirtualMachines(IResourceGroup resourceGroup)
        {
            return _azure.VirtualMachines.ListByResourceGroup(resourceGroup.Name)
                .Select(v => new VirtualMachine(v));
        }

        private IEnumerable<VirtualMachineCustomImage> ReadVirtualMachineCustomImages(IResourceGroup resourceGroup)
        {
            return _azure.VirtualMachineCustomImages.ListByResourceGroup(resourceGroup.Name)
                .Select(v => new VirtualMachineCustomImage(v));
        }

        private IEnumerable<VirtualMachineScaleSet> ReadVirtualMachineScaleSets(IResourceGroup resourceGroup)
        {
            return _azure.VirtualMachineScaleSets.ListByResourceGroup(resourceGroup.Name)
                .Select(v => new VirtualMachineScaleSet(v));
        }

        private IEnumerable<LoadBalancer> ReadLoadBalancers(IResourceGroup resourceGroup)
        {
            return _azure.LoadBalancers.ListByResourceGroup(resourceGroup.Name)
                .Select(l => new LoadBalancer(l));
        }

        private IEnumerable<KeyVault> ReadKeyVaults(IResourceGroup resourceGroup)
        {
            return _azure.Vaults.ListByResourceGroup(resourceGroup.Name)
                .Select(v => new KeyVault(v) { Uri = v.VaultUri });
        }

        private List<EventHubsNamespace> ReadEventHubsNamespaces(IResourceGroup resourceGroup)
        {
            var eventHubsNamespaces = new List<EventHubsNamespace>();

            var azureEventHubNamespaces = _azure.EventHubNamespaces.ListByResourceGroup(resourceGroup.Name);
            foreach (var azureEventHubNamespace in azureEventHubNamespaces)
            {
                EventHubsNamespace eventHubsNamespace = new EventHubsNamespace(azureEventHubNamespace);
                var azureEeventHubs =
                    _azure.EventHubs.ListByNamespace(resourceGroup.Name, azureEventHubNamespace.Name);
                foreach (var azureEventHub in azureEeventHubs)
                {
                    EventHub eventHub = new EventHub(azureEventHub.Name);
                    foreach (var azureConsumerGroup in azureEventHub.ListConsumerGroups())
                    {
                        eventHub.ConsumerGroups.Add(new ConsumerGroup { Name = azureConsumerGroup.Name });
                    }
                    eventHubsNamespace.EventHubs.Add(eventHub);
                }
                eventHubsNamespaces.Add(eventHubsNamespace);
            }
            return eventHubsNamespaces;
        }

        private List<SqlServer> ReadSqlDatabases(IResourceGroup resourceGroup)
        {
            var sqlServers = new List<SqlServer>();
            var azureSqlServers = _azure.SqlServers.ListByResourceGroup(resourceGroup.Name);
            foreach (var azureSqlServer in azureSqlServers)
            {
                var sqlServer = new SqlServer(azureSqlServer);
                foreach (var database in azureSqlServer.Databases.List())
                {
                    //TODO Ignore "master" database?
                    sqlServer.Databases.Add(new SqlDatabase { Name = database.Name });
                }
                sqlServers.Add(sqlServer);
            }
            return sqlServers;
        }

        private List<StorageAccount> ReadStorageAccounts(IResourceGroup resourceGroup)
        {
            var storageAccounts = new List<StorageAccount>();
            var azureStorageAccounts = _azure.StorageAccounts.ListByResourceGroup(resourceGroup.Name);
            foreach (var azureStorageAccount in azureStorageAccounts)
            {
                storageAccounts.Add(new StorageAccount(azureStorageAccount));
            }
            return storageAccounts;
        }

        private List<ServiceBusNamespace> ReadServiceBusNamespaces(IResourceGroup resourceGroup)
        {
            var serviceBusNamespaces = new List<ServiceBusNamespace>();
            try
            {
                var azureServiceBusNamespaces = _azure.ServiceBusNamespaces.ListByResourceGroup(resourceGroup.Name);
                foreach (var azureServiceBusNamespace in azureServiceBusNamespaces)
                {
                    var serviceBusNamespace = new ServiceBusNamespace(azureServiceBusNamespace);
                    foreach (var queue in azureServiceBusNamespace.Queues.List())
                    {
                        serviceBusNamespace.AddQueue(new ServiceBusQueue { Name = queue.Name });
                    }
                    foreach (var topic in azureServiceBusNamespace.Topics.List())
                    {
                        serviceBusNamespace.AddTopic(new ServiceBusTopic { Name = topic.Name });
                    }
                    serviceBusNamespaces.Add(serviceBusNamespace);
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
            };
            return serviceBusNamespaces;
        }

        private List<SearchService> ReadSearchServices(IResourceGroup resourceGroup)
        {
            var searchServices = new List<SearchService>();

            var azureSearchServices = _azure.SearchServices.ListByResourceGroup(resourceGroup.Name);
            foreach (var azureSearchService in azureSearchServices)
            {
                var searchService = new SearchService(azureSearchService);
                searchService.Indexes?.AddRange(searchService.Indexes.Select(s => new SearchServiceIndex { Name = s.Name }));


                searchServices.Add(searchService);
            }
            return searchServices;
        }

        private List<CosmosDBAccount> ReadCosmosDBAccounts(IResourceGroup resourceGroup)
        {
            var cosmosDBAccounts = new List<CosmosDBAccount>();

            var azureCosmosDBAccounts = _azure.CosmosDBAccounts.ListByResourceGroup(resourceGroup.Name);
            foreach (var azureCosmosDBAccount in azureCosmosDBAccounts)
            {
                cosmosDBAccounts.Add(new CosmosDBAccount(azureCosmosDBAccount));
            }
            return cosmosDBAccounts;
        }

        private List<RedisCache> ReadRedisCaches(IResourceGroup resourceGroup)
        {
            var redisCaches = new List<RedisCache>();

            var azureRedisCaches = _azure.RedisCaches.ListByResourceGroup(resourceGroup.Name);
            foreach (var azureRedisCache in azureRedisCaches)
            {
                redisCaches.Add(new RedisCache(azureRedisCache));
            }
            return redisCaches;
        }

    }
}
