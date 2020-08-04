using Authorize.App.ServiceGateways.Options;
using Authorize.App.ServiceGateways.Providers;
using Authorize.App.ServiceGateways.ServiceContracts;
using Authorize.App.ServiceGateways.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Viseo.API.Servicegateway.Contracts;
using Viseo.API.Servicegateway.IoC;
using Viseo.Web.HttpClientExtensions.IoC;

namespace Authorize.App.ServiceGateways.IoC
{
    public static class AutorizeServiceCollectionServiceGatewayExtensions
    {
        public static IServiceCollection AddAutorizeAppServiceGateway(this IServiceCollection services, IConfiguration configuration, string name)
        {
            return services
               .AddAuthorizeService()
               .AddPolicies(configuration)
               .AddTransient<IAuthenticationTokenProvider, AuthenticationTokenProvider>()
               .ApiAddAuthentication<ServiceContractsInter.IUsersServiceGateway, AuthorizeOptions>(name)
               .Services;
        }

        public static IServiceCollection AddAutorizeAppGatewayWithCache(this IServiceCollection services, IConfiguration configuration, string name)
        {
            return services
               .AddAuthorizeService()
               .AddPolicies(configuration)
               .AddTransient<IAuthenticationTokenProvider, AuthenticationTokenProvider>()
               .ApiAddAuthenticationWithCache<ServiceContractsInter.IUsersServiceGateway, AuthorizeOptions>(name)
               .Services;
        }

        public static IServiceCollection AddAuthorizeService(this IServiceCollection services)
        {
            return services
               .AddTransient<IUsersServiceGateway, UsersServiceGateway>();
        }
    }
}
