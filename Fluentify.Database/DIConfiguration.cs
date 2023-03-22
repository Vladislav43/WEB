using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fluentify.Database.Interfaces;
using Fluentify.Database.Services;
using Fluentify.Models.Database.Tables;

namespace Fluentify.Database
{
    public static class DIConfiguration
    {
        public static void RegisterDatabaseDependencies(this IServiceCollection services, IConfigurationRoot configuration) 
        {
            services.AddDbContext<StoreDbContext>((x) => x.UseSqlServer(configuration.GetConnectionString("StoreDatabase")));
            services.AddScoped(typeof(IDbEntityService<>), typeof(DbEntityService<>));
            services.AddScoped<UserService, UserService>();
        }
    }
}
