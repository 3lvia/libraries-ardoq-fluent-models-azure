using System.Collections.Generic;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace ArdoqFluentModels.Azure.Model
{
    public class ResourceGroup : ResourceBase
    {
        public List<EventHubsNamespace> EventHubsNamespaces { get; } = new List<EventHubsNamespace>();
        public List<SqlServer> SqlServers { get; } = new List<SqlServer>();
        public List<StorageAccount> StorageAccounts { get; } = new List<StorageAccount>();
        public List<ServiceBusNamespace> ServiceBusNamespaces { get; } = new List<ServiceBusNamespace>();
        public List<SearchService> SearchServices { get; } = new List<SearchService>();
        public List<CosmosDBAccount> CosmosDBAccounts { get; } = new List<CosmosDBAccount>();
        public List<RedisCache> RedisCaches { get; } = new List<RedisCache>();
        public List<KeyVault> KeyVaults { get; } = new List<KeyVault>();

        public List<AlertRule> AlertRules { get; } = new List<AlertRule>();
        public List<AccessManagement> AccessManagements { get; } = new List<AccessManagement>();
        public List<ApplicationGateway> ApplicationGateways { get; } = new List<ApplicationGateway>();
        public List<AutoscaleSetting> AutoscaleSettings { get; } = new List<AutoscaleSetting>();
        public List<AvailabilitySet> AvailabilitySets { get; } = new List<AvailabilitySet>();
        public List<BatchAccount> BatchAccounts { get; } = new List<BatchAccount>();
        public List<CdnProfile> CdnProfiles { get; } = new List<CdnProfile>();
        public List<ComputeSku> ComputeSkus { get; } = new List<ComputeSku>();
        public List<Disk> Disks { get; } = new List<Disk>();
        public List<DnsZone> DnsZones { get; } = new List<DnsZone>();
        public List<NetworkSecurityGroup> NetworkSecurityGroups { get; } = new List<NetworkSecurityGroup>();
        public List<VirtualNetwork> VirtualNetworks { get; } = new List<VirtualNetwork>();
        public List<PublicIPAddress> PublicIPAddresses { get; } = new List<PublicIPAddress>();
        public List<Snapshot> Snapshots { get; } = new List<Snapshot>();
        public List<TrafficManagerProfile> TrafficManagerProfiles { get; } = new List<TrafficManagerProfile>();
        public List<VirtualMachine> VirtualMachines { get; } = new List<VirtualMachine>();
        public List<VirtualMachineCustomImage> VirtualMachineCustomImages { get; } = new List<VirtualMachineCustomImage>();
        public List<VirtualMachineScaleSet> VirtualMachineScaleSets { get; } = new List<VirtualMachineScaleSet>();
        public List<LoadBalancer> LoadBalancers { get; } = new List<LoadBalancer>();

        public ResourceGroup(IResource resource) : base(resource)
        {
        }

        public void AppendToFlattenedStructure(List<object> list)
        {
            list.Add(this);

            foreach (var eventHubNamespace in EventHubsNamespaces)
            {
                eventHubNamespace.AppendToFlattenedStructure(list);
            }

            foreach (var searchService in SearchServices)
            {
                searchService.AppendToFlattenedStructure(list);
            }

            foreach (var server in SqlServers)
            {
                server.AppendToFlattenedStructure(list);
            }

            foreach (var serviceBusNamespace in ServiceBusNamespaces)
            {
                serviceBusNamespace.AppendToFlattenedStructure(list);
            }
            
            list.AddRange(StorageAccounts);
            list.AddRange(CosmosDBAccounts);
            list.AddRange(RedisCaches);
            list.AddRange(KeyVaults);

            // Have to update template i Ardoq and GetBuilder for Azure. ex. builder.AddComponentMapping<ApplicationGateway>("Application gateway") ...
            list.AddRange(ApplicationGateways);
            list.AddRange(AutoscaleSettings);
            list.AddRange(AvailabilitySets);
            list.AddRange(BatchAccounts);
            list.AddRange(CdnProfiles);
            list.AddRange(Disks);
            list.AddRange(DnsZones);

            foreach (var networkSecurityGroup in NetworkSecurityGroups)
            {
                networkSecurityGroup.AppendToFlattenedStructure(list);
            }

            list.AddRange(VirtualNetworks);
            list.AddRange(PublicIPAddresses);
            list.AddRange(Snapshots);
            list.AddRange(TrafficManagerProfiles);
            list.AddRange(VirtualMachines);
            list.AddRange(VirtualMachineCustomImages);
            list.AddRange(VirtualMachineScaleSets);
            list.AddRange(LoadBalancers);
        }
    }
}
