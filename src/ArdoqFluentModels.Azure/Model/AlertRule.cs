using System.Collections.Generic;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace ArdoqFluentModels.Azure.Model
{
    public class AlertRule : ResourceBase
    {
        public List<MetricAlert> MetricAlerts { get; } = new List<MetricAlert>();

        public List<ActivityLogAlert> ActivityLogAlerts { get; } = new List<ActivityLogAlert>();
        

        public AlertRule(IResource resource) : base(resource) { }
    }
}
