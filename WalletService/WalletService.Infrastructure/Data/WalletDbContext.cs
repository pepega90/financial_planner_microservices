using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletService.Domain.Models;

namespace WalletService.Infrastructure.Data
{
    public class WalletDbContext : DbContext
    {
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Category> Categories { get; set; }
        public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Wallet - Record relationship (one-to-many)
            modelBuilder.Entity<Wallet>()
                .HasMany(w => w.Records)
                .WithOne()
                .HasForeignKey("WalletId")
                .OnDelete(DeleteBehavior.Cascade);

            // Record - Category relationship (many-to-one)
            modelBuilder.Entity<Record>()
                .HasOne(e => e.Category)
                .WithMany()
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure TransactionType as enum
            modelBuilder.Entity<Record>()
                .Property(r => r.Type)
                .HasConversion<string>();
        }
    }
}
