using Authorize.ServiceGateways.Options;
using Authorize.ServiceGateways.Providers;
using Authorize.ServiceGateways.ServiceContracts;
using Authorize.ServiceGateways.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Viseo.API.Servicegateway.Contracts;
using Viseo.API.Servicegateway.IoC;
using Viseo.Web.HttpClientExtensions.IoC;

namespace Authorize.ServiceGateways.IoC
{
    public static class AutorizeServiceCollectionServiceGatewayExtensions
    {
        public static IServiceCollection AddAutorizeServiceGateway(this IServiceCollection services, IConfiguration configuration, string name)
        {
            return services
               .AddAuthorizeService()
               .AddPolicies(configuration)
               .AddTransient<IAuthenticationTokenProvider, AuthenticationTokenProvider>()
               .ApiAddAuthentication<ServiceContractsInter.IRolesServiceGateway, AuthorizeOptions>(name)
               .Services
               .ApiAddAuthentication<ServiceContractsInter.IUsersServiceGateway, AuthorizeOptions>(name)
               .Services
               .ApiAddAuthentication<ServiceContractsInter.IApplicationsServiceGateway, AuthorizeOptions>(name)
               .Services;
        }

        public static IServiceCollection AddAutorizeGatewayWithCache(this IServiceCollection services, IConfiguration configuration, string name)
        {

            return services
               .AddAuthorizeService()
               .AddPolicies(configuration)
               .AddTransient<IAuthenticationTokenProvider, AuthenticationTokenProvider>()
               .ApiAddAuthenticationWithCache<ServiceContractsInter.IRolesServiceGateway, AuthorizeOptions>(name)
               .Services
               .ApiAddAuthenticationWithCache<ServiceContractsInter.IUsersServiceGateway, AuthorizeOptions>(name)
               .Services
               .ApiAddAuthenticationWithCache<ServiceContractsInter.IApplicationsServiceGateway, AuthorizeOptions>(name)
               .Services;
        }

        public static IServiceCollection AddAuthorizeService(this IServiceCollection services)
        {
            return services
               .AddScoped<IRolesServiceGateway, RolesServiceGateway>()
               .AddScoped<IUsersServiceGateway, UsersServiceGateway>()
               .AddScoped<IApplicationsServiceGateway, ApplicationsServiceGateway>();
        }
    }
}
