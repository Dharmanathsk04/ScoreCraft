using Microsoft.AspNetCore.Routing;

namespace Shared.Core;
public interface IBaseEndpoint
{
    void MapEndpoints(IEndpointRouteBuilder routeBuilder);
}
