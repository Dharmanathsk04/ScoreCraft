using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Shared.Core.Extensions;
public static class EndpointMappingExtension
{
    public static IServiceCollection RegisterEndpoints(this IServiceCollection services, List<Assembly> mediatRAssembly)
    {
        var serviceDescriptors = new List<ServiceDescriptor>();

        mediatRAssembly.ForEach(assemblyDetails =>
        {
            var descriptors = assemblyDetails.DefinedTypes
                .Where(type => type is {IsAbstract: false, IsInterface: false} &&
                               type.IsAssignableTo(typeof(IBaseEndpoint)))
                .Select(type => ServiceDescriptor.Transient(typeof(IBaseEndpoint), type))
                .ToList();

            serviceDescriptors.AddRange(descriptors);
        });

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    /// <summary>
    /// Map end-points
    /// </summary>
    /// <param name="app">The web application</param>
    /// <param name="routeGroupBuilder">If we need any route group builder to register end-point. Example: API Versioning</param>
    /// <returns></returns>
    public static IApplicationBuilder MapEndpoints(
        this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        var endpoints = app.Services
            .GetRequiredService<IEnumerable<IBaseEndpoint>>();

        IEndpointRouteBuilder builder =
            routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoints(builder);
        }

        return app;
    }
}
