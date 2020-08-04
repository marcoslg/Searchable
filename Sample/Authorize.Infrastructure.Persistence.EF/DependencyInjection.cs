using Authorize.Application.Contracts;
using Common.Infrastructure.EF.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ML.Data.Contracts.Respositories;
using ML.Data.Contracts.UnitOfWork;
using ML.Infrastructure.Persistence.EF.Repositories;

namespace Authorize.Infrastructure.Persistence.EF
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructurePersitence(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase", false))
            {
                services.AddDbContext<AuthorizeDbContext>(options =>
                    options.UseInMemoryDatabase("AuthorizeDB"));
            }
            else if (configuration.GetValue<bool>("UseCosmos", false))
            {
                services.AddDbContext<AuthorizeDbContext>(options =>
                   options.UseCosmos(
                       configuration.GetConnectionString("DefaultConnection"),
                       configuration.GetConnectionString("CosmosKey"),
                       "AuthorizeDB"));
            }
            else
            {
                services.AddDbContext<AuthorizeDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        providerOptions => {
                            providerOptions.MigrationsAssembly(typeof(AuthorizeDbContext).Assembly.FullName);
                            providerOptions.EnableRetryOnFailure();
                        }));
            }
            services.AddScoped<IAppDbContext, AuthorizeDbContext>(sp => sp.GetService<AuthorizeDbContext>());          
            
            services.AddScoped<IUnitOfWork, UnitOfWorkBase>(sp => new UnitOfWorkBase (sp.GetService<AuthorizeDbContext>()));            
            services.AddScoped<DbContext>(sp => sp.GetService<AuthorizeDbContext>());            
            services.AddTransient(typeof(IQueryRepository<>), typeof(QueryRepository<>));
            
                      

            return services;
        }
    }
}
