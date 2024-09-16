using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletService.Domain.Models;
using WalletService.Infrastructure.Data;

namespace WalletService.Infrastructure.Repositories
{
    public class RecordRepository : IRecordRepo
    {
        private readonly WalletDbContext _dbContext;
        public RecordRepository(WalletDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Record> CreateRecord(Record request)
        {
            var wallet = await _dbContext.Wallets.FindAsync(request.WalletId);
            var record = new Record
            {
                WalletId = wallet.Id,
                Amount = request.Amount,
                Tanggal = DateTime.UtcNow,
                Type = request.Type,
                CategoryId = request.CategoryId,
            };
            
            var createdRecord = _dbContext.Records.Add(record);

            wallet.Records.Add(createdRecord.Entity);
            
            wallet.Balance += request.Type == TransactionType.Income ? request.Amount : -request.Amount;
            
            await _dbContext.SaveChangesAsync();

            return createdRecord.Entity;
        }
    }
}
