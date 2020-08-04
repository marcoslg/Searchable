using Authorize.Application.Behaviours;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Authorize.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuthorizeApplication(this IServiceCollection services)
        => services
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddSingleton<IAuthConfiguration, AuthConfiguration>()
            .AddSingleton<IAuthPermissions, AuthPermissions>()
            //.AddTransient(typeof(IRequestPreProcessor<>), typeof(RequestAuthPreProcessorBehavior<>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            //.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));

    }
}
