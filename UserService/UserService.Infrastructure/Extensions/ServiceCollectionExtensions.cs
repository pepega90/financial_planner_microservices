using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Interfaces;
using UserService.Domain.Models;
using UserService.Infrastructure.Data;
using UserService.Infrastructure.Repositories;

namespace UserService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(opt =>
            {
                opt.UseNpgsql(
                    configuration.GetConnectionString("Db")
                );
            });
            services.AddScoped<IUserRepo, UserRepository>();
            services.AddScoped<IUserService, UserService.Application.Services.UserService>();

            return services;
        }
    }
}
