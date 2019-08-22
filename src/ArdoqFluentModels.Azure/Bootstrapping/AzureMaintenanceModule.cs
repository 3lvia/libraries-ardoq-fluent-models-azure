using ArdoqFluentModels.Azure.Model;
using ArdoqFluentModels.Mapping;
using ArdoqFluentModels.Mapping.ComponentHierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArdoqFluentModels.Azure.Bootstrapping
{
    public class AzureMaintenanceModule : IMaintenanceModule
    {
        public void Configure(ArdoqModelMappingBuilder builder)
        {
            // Subscription
            builder.AddComponentMapping<Subscription>("Subscription")
                .WithKey(s => s.Name)
                .WithModelledHierarchyReference(s => s.ResourceGroups, ModelledReferenceDirection.Parent);

            // Resource Group
            builder.AddComponentMapping<ResourceGroup>("Resource group")
                .WithKey(rg => rg.Name)
                .WithModelledHierarchyReference(rg => rg.EventHubsNamespaces, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.SqlServers, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.StorageAccounts, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.ServiceBusNamespaces, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.SearchServices, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.CosmosDBAccounts, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.RedisCaches, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.KeyVaults, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.AccessManagements, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.AlertRules, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.ApplicationGateways, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.AutoscaleSettings, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.AvailabilitySets, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.BatchAccounts, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.CdnProfiles, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.ComputeSkus, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.Disks, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.DnsZones, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.LoadBalancers, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.NetworkSecurityGroups, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.PublicIPAddresses, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.Snapshots, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.TrafficManagerProfiles, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.VirtualMachines, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.VirtualMachineCustomImages, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.VirtualMachineScaleSets, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rg => rg.VirtualNetworks, ModelledReferenceDirection.Parent)
                .WithTags(Tags.FromExpression<ResourceGroup>(
                    rg => rg.Tags?.Select(t => $"{t.Key}-{t.Value}") ?? new List<string>()));

            // Event Hubs Namespace
            builder.AddComponentMapping<EventHubsNamespace>("Event Hubs Namespace")
                .WithKey(ehn => ehn.Name)
                .WithModelledHierarchyReference(ehn => ehn.EventHubs, ModelledReferenceDirection.Parent)
                .WithTags(Tags.FromExpression<EventHubsNamespace>(
                    rg => rg.Tags?.Select(t => $"{t.Key}-{t.Value}") ?? new List<string>()));

            builder.AddComponentMapping<EventHub>("Event Hub")
                .WithKey(eh => eh.Name)
                .WithModelledHierarchyReference(eh => eh.ConsumerGroups, ModelledReferenceDirection.Parent);

            builder.AddComponentMapping<ConsumerGroup>("Consumer Group")
                .WithKey(cg => cg.Name);

            // SQL
            builder.AddComponentMapping<SqlServer>("SQL server")
                .WithKey(s => s.Name)
                .WithModelledHierarchyReference(s => s.Databases, ModelledReferenceDirection.Parent)
                .WithTags(Tags.FromExpression<SqlServer>(
                    rg => rg.Tags?.Select(t => $"{t.Key}-{t.Value}") ?? new List<string>()));

            builder.AddComponentMapping<SqlDatabase>("SQL database")
                .WithKey(s => s.Name);

            // Storage
            builder.AddComponentMapping<StorageAccount>("Storage account")
                .WithKey(s => s.Name)
                .WithTags(Tags.FromExpression<StorageAccount>(
                    rg => rg.Tags?.Select(t => $"{t.Key}-{t.Value}") ?? new List<string>()));

            // Service Bus
            builder.AddComponentMapping<ServiceBusNamespace>("Service Bus Namespace")
                .WithKey(sbn => sbn.Name)
                .WithField(sbn => sbn.EndpointUrl, "uri")
                .WithModelledHierarchyReference(sbn => sbn.Queues, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(sbn => sbn.Topics, ModelledReferenceDirection.Parent)
                .WithTags(Tags.FromExpression<ServiceBusNamespace>(
                    rg => rg.Tags?.Select(t => $"{t.Key}-{t.Value}") ?? new List<string>()));

            builder.AddComponentMapping<ServiceBusTopic>("Topic")
                .WithKey(t => t.Name);

            builder.AddComponentMapping<ServiceBusQueue>("Queue")
                .WithKey(q => q.Name);

            // Search
            builder.AddComponentMapping<SearchService>("Search Service")
                .WithKey(s => s.Name)
                .WithModelledHierarchyReference(s => s.Indexes, ModelledReferenceDirection.Parent)
                .WithTags(Tags.FromExpression<SearchService>(
                    rg => rg.Tags?.Select(t => $"{t.Key}-{t.Value}") ?? new List<string>()));

            builder.AddComponentMapping<SearchServiceIndex>("Search Service Index")
                .WithKey(s => s.Name);

            // Cosmos DB
            builder.AddComponentMapping<CosmosDBAccount>("Cosmos DB account")
                .WithKey(c => c.Name)
                .WithTags(Tags.FromExpression<CosmosDBAccount>(
                    rg => rg.Tags?.Select(t => $"{t.Key}-{t.Value}") ?? new List<string>()));

            // Redis
            builder.AddComponentMapping<RedisCache>("Redis Cache")
                .WithKey(r => r.Name)
                .WithTags(Tags.FromExpression<RedisCache>(
                    rg => rg.Tags?.Select(t => $"{t.Key}-{t.Value}") ?? new List<string>()));

            // Key Vault
            builder.AddComponentMapping<KeyVault>("Key vault")
                .WithKey(kv => kv.Name)
                .WithField(kv => kv.Uri, "uri")
                .WithTags(Tags.FromExpression<KeyVault>(
                    rg => rg.Tags?.Select(t => $"{t.Key}-{t.Value}") ?? new List<string>()));

            // HERIFRA!!
            // Access Management
            builder.AddComponentMapping<AccessManagement>("Access Management")
                .WithKey(am => am.Name)
                .WithModelledHierarchyReference(am => am.ActiveDirectoryGroups, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(am => am.ActiveDirectoryApplications, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(am => am.ActiveDirectoryUsers, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(am => am.RoleAssignments, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(am => am.RoleDefinitions, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(am => am.ServicePrincipals, ModelledReferenceDirection.Parent);

            builder.AddComponentMapping<ActiveDirectoryGroup>("Active Directory Group")
                .WithKey(adg => adg.Name);
            builder.AddComponentMapping<ActiveDirectoryApplication>("Active Directory Application")
                .WithKey(ada => ada.Name);
            builder.AddComponentMapping<ActiveDirectoryUser>("Active Directory User")
                .WithKey(adu => adu.Name);
            builder.AddComponentMapping<RoleAssignment>("Role Assignment")
                .WithKey(ra => ra.Name);
            builder.AddComponentMapping<RoleDefinition>("Role Definition")
                .WithKey(rd => rd.Name);
            builder.AddComponentMapping<ServicePrincipal>("Service Principal")
                .WithKey(sp => sp.Name);

            // AlertRule
            builder.AddComponentMapping<AlertRule>("Alert Rule")
                .WithKey(rd => rd.Name)
                .WithModelledHierarchyReference(rd => rd.MetricAlerts, ModelledReferenceDirection.Parent)
                .WithModelledHierarchyReference(rd => rd.ActivityLogAlerts, ModelledReferenceDirection.Parent);

            builder.AddComponentMapping<MetricAlert>("Metric Alert")
                .WithKey(ma => ma.Name);
            builder.AddComponentMapping<ActivityLogAlert>("Activity Log Alert")
                .WithKey(ala => ala.Name);

            // ApplicationGateway
            builder.AddComponentMapping<ApplicationGateway>("Application Gateway")
                .WithKey(ag => ag.Name);

            // AutoscaleSetting
            builder.AddComponentMapping<AutoscaleSetting>("Autoscale Setting")
                .WithKey(ass => ass.Name);

            // AvailabilitySet
            builder.AddComponentMapping<AvailabilitySet>("Availability Set")
                .WithKey(avs => avs.Name);

            // BatchAccount
            builder.AddComponentMapping<BatchAccount>("Batch Account")
                .WithKey(ba => ba.Name);

            // CdnProfile
            builder.AddComponentMapping<CdnProfile>("Cdn Profile")
                .WithKey(cp => cp.Name);

            // ComputeSku
            builder.AddComponentMapping<ComputeSku>("Compute Sku")
                .WithKey(cs => cs.Name);

            // Deployment
            builder.AddComponentMapping<Deployment>("Deployment")
                .WithKey(d => d.Name);

            // Disk
            builder.AddComponentMapping<Disk>("Disk")
                .WithKey(d => d.Name);

            // DnsZone
            builder.AddComponentMapping<DnsZone>("Dns Zone")
                .WithKey(dz => dz.Name);

            // LoadBalancer
            builder.AddComponentMapping<LoadBalancer>("Load Balancer")
                .WithKey(lb => lb.Name);

            // NetworkSecurityGroup
            builder.AddComponentMapping<NetworkSecurityGroup>("Network SecurityGroup")
                .WithKey(nsg => nsg.Name)
                .WithModelledHierarchyReference(nsg => nsg.NetworkInterfaces, ModelledReferenceDirection.Parent);

            builder.AddComponentMapping<NetworkInterface>("Network Interface")
                .WithKey(li => li.Name);

            // PublicIPAddress
            builder.AddComponentMapping<PublicIPAddress>("Public IP Address")
                .WithKey(pipa => pipa.Name);

            // Snapshot
            builder.AddComponentMapping<Snapshot>("Snapshot")
                .WithKey(ss => ss.Name);

            // TrafficManagerProfile
            builder.AddComponentMapping<TrafficManagerProfile>("Traffic Manager Profile")
                .WithKey(tmp => tmp.Name);

            // VirtualMachine
            builder.AddComponentMapping<VirtualMachine>("Virtual Machine")
                .WithKey(vm => vm.Name);

            // VirtualMachineCustomImage
            builder.AddComponentMapping<VirtualMachineCustomImage>("Virtual Machine Custom Image")
                .WithKey(vmci => vmci.Name);

            // VirtualMachineScaleSet
            builder.AddComponentMapping<VirtualMachineScaleSet>("Virtual Machine Scale Set")
                .WithKey(vmss => vmss.Name);

            // VirtualNetwork
            builder.AddComponentMapping<VirtualNetwork>("Virtual Network")
                .WithKey(vn => vn.Name);
        }
    }
}
