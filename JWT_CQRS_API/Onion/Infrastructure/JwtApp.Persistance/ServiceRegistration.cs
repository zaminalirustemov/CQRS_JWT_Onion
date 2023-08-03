using JwtApp.Application.Interfaces;
using JwtApp.Persistance.Context;
using JwtApp.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JwtApp.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<JwtContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Local"));
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        }
    }
}
