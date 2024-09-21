using Microsoft.EntityFrameworkCore;
using TransactionService.Grpc.Data;

namespace TransactionService.Grpc.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TransDbContext>(opt =>
            {
                opt.UseNpgsql(
                    configuration.GetConnectionString("Db")
                );
            });
            return services;
        }
    }
}
