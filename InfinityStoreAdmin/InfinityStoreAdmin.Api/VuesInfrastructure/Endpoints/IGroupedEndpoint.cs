namespace InfinityStoreAdmin.Api.VuesInfrastructure.Endpoints;

public interface IGroupedEndpoint
{
    public string ApiGroup { get; }
    public void DefineEndpoint(RouteGroupBuilder app);
}