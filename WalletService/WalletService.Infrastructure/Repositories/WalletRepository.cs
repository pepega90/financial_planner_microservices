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
                .ToListAsync();
            return res;
        }

        public async Task TransferBetweenWallet(Guid fromWalletId, Guid toWalletId, double amount)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var fromWallet = await _dbContext.Wallets.FindAsync(fromWalletId);
                var toWallet = await _dbContext.Wallets.FindAsync(toWalletId);

                if(fromWallet is null || toWallet is null)
                {
                    throw new Exception("One of the wallet is not found!");
                }

                if (fromWallet.Balance < (decimal)amount) throw new Exception("Amount melebihi balance!");

                fromWallet.Balance -= (decimal)amount;
                toWallet.Balance += (decimal)amount;

                var transferRecord = new Record
                {
                    WalletId = fromWalletId,
                    Amount = (decimal)amount,
                    CategoryId = 2,
                    Tanggal = DateTime.UtcNow,
                    Type = TransactionType.Expense,
                };

                var receivedRecord = new Record
                {
                    WalletId = toWalletId,
                    Amount = (decimal)amount,
                    CategoryId = 2,
                    Tanggal = DateTime.UtcNow,
                    Type = TransactionType.Income,
                };

                _dbContext.Records.Add(transferRecord);
                _dbContext.Records.Add(receivedRecord);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error ketika transfer = {ex.Message}");
            }
        }
    }
}
