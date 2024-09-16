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
    }
}
