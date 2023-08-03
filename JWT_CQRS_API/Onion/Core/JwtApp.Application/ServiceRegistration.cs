using AutoMapper;
using JwtApp.Application.Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JwtApp.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(opt =>
            {
                opt.AddProfiles(new List<Profile>
                {
                    new CategoryProfile(),
                    new ProductProfile(),
                });
            });
        }
    }
}
