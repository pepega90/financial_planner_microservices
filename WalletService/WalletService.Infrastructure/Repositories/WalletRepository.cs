using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletService.Domain.Models;
using WalletService.Infrastructure.Data;

namespace WalletService.Infrastructure.Repositories
{
    internal class WalletRepository : IWalletRepo
    {
        private readonly WalletDbContext _dbContext;
        public WalletRepository(WalletDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Wallet> CreateWallet(Wallet wallet)
        {
            var res = await _dbContext.Wallets.AddAsync(wallet);
            await _dbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<List<Wallet>> GetWalletByUserId(Guid userId)
        {
            var res = await _dbContext.Wallets
                .Include(w => w.Records)
                .ThenInclude(c => c.Category)
                .Where(e => e.UserId == userId)
                .ToListAsync(); ;
            return res;
        }
    }
}
