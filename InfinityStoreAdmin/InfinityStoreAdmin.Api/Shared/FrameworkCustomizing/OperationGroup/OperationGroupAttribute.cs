namespace InfinityStoreAdmin.Api.Shared.FrameworkCustomizing.OperationGroup
{
    public class OperationGroupAttribute : Attribute
    {
        public string Name { get; private set; }

        public OperationGroupAttribute(string name)
        {
            Name = name;
        }
    }
}
