namespace ArdoqFluentModels.Azure.Model
{
    public class MetricAlert
    {
        public string Name { get; }

        public MetricAlert(string name)
        {
            Name = name;
        }
    }
}
