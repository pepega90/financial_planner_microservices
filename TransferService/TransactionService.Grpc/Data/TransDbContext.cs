using Microsoft.EntityFrameworkCore;
using TransactionService.Grpc.Models;

namespace TransactionService.Grpc.Data
{
    public class TransDbContext : DbContext
    {
        public DbSet<Transfer> Transfers { get; set; }
        public TransDbContext(DbContextOptions<TransDbContext> options) : base(options) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
