using WalletService.Application.DTOs;
using WalletService.Domain.Models;

namespace WalletService.Application.Interfaces
{
    public interface IRecordService
    {
        Task<Record> CreateRecord(CreateRecordDto request);
        Task<List<Record>> GetRecordsBetweenDates(Guid walletId, DateTime startTime, DateTime endTime);
        (decimal TotalIncome, decimal TotalExpense) GetCashFlow(DateTime startTime, DateTime endTime);
        Dictionary<string, decimal> GetExpenseRecapByCategory(DateTime startTime, DateTime endTime);
        List<Record> GetLastRecords(int count);
    }
}
