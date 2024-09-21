using SharedService.Messaging.Messages;
using WalletService.Application.DTOs;
using WalletService.Domain.Models;

namespace WalletService.Application.Interfaces
{
    public interface IWalletService
    {
        Task<Wallet> CreateWallet(CreateWalletDto createWalletDto);
        Task CreateDefaultWalletUser(UserCreatedMessage message);
        Task<List<Wallet>> GetWalletByUserId(Guid userId);
        Task TransferBetweenWallet(Guid fromWalletId, Guid toWalletId, double amount);
    }
}
