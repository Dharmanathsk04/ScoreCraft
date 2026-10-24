using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Quiz.Application;
using Modules.Quiz.Infrastructure;
using Serilog;

namespace Modules.Quiz.Endpoints;
public static class QuestionModuleEndpointsServiceCollectionExtension
{
    public static IServiceCollection RegisterQuestionModule(this IServiceCollection services,
        IConfiguration configuration,
        ILogger logger,
        List<Assembly> mediatRAssembly)
    {
        mediatRAssembly.Add(typeof(QuestionModuleEndpointsServiceCollectionExtension).Assembly);
        services.RegisterQuestionModuleApplication(mediatRAssembly);
        services.RegisterQuestionModuleInfrastructure(logger, configuration, mediatRAssembly);
        return services;
    }


}
