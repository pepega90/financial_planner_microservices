using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WalletService.Application.Consumers;
using WalletService.Application.Interfaces;
using WalletService.Domain.Models;
using WalletService.Infrastructure.Data;
using WalletService.Infrastructure.Repositories;

namespace WalletService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WalletDbContext>(opt =>
            {
                opt.UseNpgsql(
                    configuration.GetConnectionString("Db")
                );
            });
            services.AddScoped<IWalletRepo, WalletRepository>();
            services.AddScoped<IWalletService, WalletService.Application.Services.WalletService>();
            services.AddScoped<IRecordRepo, RecordRepository>();
            services.AddScoped<IRecordService, WalletService.Application.Services.RecordService>();

            return services;
        }
    }
}
