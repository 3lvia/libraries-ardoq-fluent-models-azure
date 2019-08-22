namespace ArdoqFluentModels.Azure.Model
{
    public class ServicePrincipal
    {
        public string Name { get; }

        public ServicePrincipal(string name)
        {
            Name = name;
        }
    }
}
