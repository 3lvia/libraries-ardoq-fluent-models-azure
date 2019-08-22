using ArdoqFluentModels.Azure.Model;

namespace ArdoqFluentModels.Azure
{
    public interface IAzureReader
    {
        string SubscriptionName { get; }

        Subscription ReadSubscription();
    }
}
