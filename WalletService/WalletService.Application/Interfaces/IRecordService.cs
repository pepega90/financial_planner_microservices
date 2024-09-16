using WalletService.Application.DTOs;
using WalletService.Domain.Models;

namespace WalletService.Application.Interfaces
{
    public interface IRecordService
    {
        Task<Record> CreateRecord(CreateRecordDto request);
    }
}
