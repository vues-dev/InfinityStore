using System;
namespace Vues.Net;

public interface IGroupedEndpoint
{
    public string ApiGroup { get; }
    public void DefineEndpoint(RouteGroupBuilder app);
}