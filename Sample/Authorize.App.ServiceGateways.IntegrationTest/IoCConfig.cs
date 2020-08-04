using Authorize.App.ServiceGateways.IoC;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Birchman.Cache;
using Birchman.Cache.OnMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Viseo.API.Servicegateway.Configuration.MSTest;

namespace Authorize.App.ServiceGateways.IntegrationTest
{
    public class IoCConfig
    {
        public static IConfiguration Configuration { get; private set; }

        public static IServiceProvider GetServiceProvider(TestContext context)
        {
            Configuration = new ConfigurationBuilder()
                .AddTestContext(context)
                .Build();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IConfiguration>(_ => Configuration);
            serviceCollection.AddScoped<IAuthenticationService, Providers.AuthenticationService>();
            serviceCollection.AddScoped<IHttpContextAccessor>(sp => new Providers.HttpContextAccessor(sp));
            serviceCollection.AddSingleton<ICacheManager>(sp => new OnMemoryCacheManager(600));
            serviceCollection.AddAutorizeAppGatewayWithCache(Configuration, "AuthorizeApi");
            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
