namespace ArdoqFluentModels.Azure.Model
{
    public class ActiveDirectoryUser
    {
        public string Name { get; }

        public ActiveDirectoryUser(string name)
        {
            Name = name;
        }
    }
}
