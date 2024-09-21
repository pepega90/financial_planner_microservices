using WalletService.Application.DTOs;
using WalletService.Application.Interfaces;
using WalletService.Domain.Models;

namespace WalletService.Application.Services
{
    public class RecordService : IRecordService
    {
        private readonly IRecordRepo _recordRepo;
        public RecordService(IRecordRepo recordRepo)
        {
            _recordRepo = recordRepo;
        }

        public async Task<Record> CreateRecord(CreateRecordDto request)
        {
            var record = new Record()
            {
                Amount = request.Amount,
                CategoryId = request.CategoryId,
                Type = Enum.Parse<TransactionType>(request.Type, true),
                WalletId = request.WalletId,
            };

            var res = await _recordRepo.CreateRecord(record);
            return res;
        }

        public (decimal TotalIncome, decimal TotalExpense) GetCashFlow(DateTime startTime, DateTime endTime)
        {
            return _recordRepo.GetCashFlow(startTime, endTime);
        }

        public Dictionary<string, decimal> GetExpenseRecapByCategory(DateTime startTime, DateTime endTime)
        {
            return _recordRepo.GetExpenseRecapByCategory(startTime, endTime);
        }

        public List<Record> GetLastRecords(int count)
        {
            return _recordRepo.GetLastRecords(count);
        }

        public Task<List<Record>> GetRecordsBetweenDates(Guid walletId, DateTime startTime, DateTime endTime)
        {
            return _recordRepo.GetRecordsBetweenDates(walletId, startTime, endTime);
        }
    }
}
