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

        public (decimal TotalIncome, decimal TotalExpense) GetCashFlow(DateTime startTime, DateTime endTime)
        {
            // Convert input DateTimes to UTC if they aren't already
            var utcStartTime = startTime.Kind == DateTimeKind.Utc ? startTime : startTime.ToUniversalTime();
            var utcEndTime = endTime.Kind == DateTimeKind.Utc ? endTime : endTime.ToUniversalTime();

            var records = _dbContext.Wallets
                .Include(r => r.Records)
                .SelectMany(w => w.Records)
                .Where(r => r.Tanggal >= utcStartTime && r.Tanggal <= utcEndTime);

            return (
                TotalIncome: records.Where(r => r.Type == TransactionType.Income).Sum(r => r.Amount),
                TotalExpense: records.Where(r => r.Type == TransactionType.Expense).Sum(r => r.Amount)
            );
        }

        public Dictionary<string, decimal> GetExpenseRecapByCategory(DateTime startTime, DateTime endTime)
        {
            var utcStartTime = startTime.Kind == DateTimeKind.Utc ? startTime : startTime.ToUniversalTime();
            var utcEndTime = endTime.Kind == DateTimeKind.Utc ? endTime : endTime.ToUniversalTime();

            return _dbContext.Wallets
            .Include(r => r.Records)
            .SelectMany(w => w.Records)
            .Where(r => r.Tanggal >= utcStartTime && r.Tanggal <= utcEndTime && r.Type == TransactionType.Expense)
            .GroupBy(r => r.Category.Name)
            .ToDictionary(g => g.Key, g => g.Sum(r => r.Amount));
        }

        public List<Record> GetLastRecords(int count)
        {
            return _dbContext.Wallets
            .Include(r => r.Records)
            .SelectMany(w => w.Records)
            .OrderByDescending(r => r.Tanggal)
            .Take(count)
            .ToList();
        }

        public async Task<List<Record>> GetRecordsBetweenDates(Guid walletId, DateTime startTime, DateTime endTime)
        {
            var wallet = await _dbContext.Wallets
                .Include(r => r.Records)
                .ThenInclude(c => c.Category)
                .FirstOrDefaultAsync(e => e.Id == walletId);
            return wallet?.Records.Where(r => r.Tanggal >= startTime && r.Tanggal <= endTime).ToList() ?? new List<Record>();
        }
    }
}
