using System.Collections.Generic;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace ArdoqFluentModels.Azure.Model
{
    public class AccessManagement : ResourceBase
    {
        public List<ActiveDirectoryGroup> ActiveDirectoryGroups { get; } = new List<ActiveDirectoryGroup>();

        public List<ActiveDirectoryApplication> ActiveDirectoryApplications { get; } = new List<ActiveDirectoryApplication>();

        public List<ActiveDirectoryUser> ActiveDirectoryUsers { get; } = new List<ActiveDirectoryUser>();

        public List<RoleAssignment> RoleAssignments { get; } = new List<RoleAssignment>();

        public List<RoleDefinition> RoleDefinitions { get; } = new List<RoleDefinition>();

        public List<ServicePrincipal> ServicePrincipals { get; } = new List<ServicePrincipal>();
        
        public AccessManagement(IResource resource) : base(resource) { }
    }
}
