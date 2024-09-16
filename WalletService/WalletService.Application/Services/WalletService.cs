using SharedService.Messaging.Messages;
using WalletService.Application.DTOs;
using WalletService.Application.Interfaces;
using WalletService.Domain.Models;

namespace WalletService.Application.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepo _walletRepo;
        public WalletService(IWalletRepo walletRepo)
        {
            _walletRepo = walletRepo;
        }

        public async Task CreateDefaultWalletUser(UserCreatedMessage message)
        {
            var wallet = new Wallet()
            {
                UserId = message.UserId,
                Balance = 0,
                Name = message.Username + " wallet",
            };

            await _walletRepo.CreateWallet(wallet);
        }

        public async Task<Wallet> CreateWallet(CreateWalletDto createWalletDto)
        {
            var wallet = new Wallet() 
            { 
                Balance = createWalletDto.Balance,
                Name = createWalletDto.Name,
                UserId = createWalletDto.UserId,
            };

            var res = await _walletRepo.CreateWallet(wallet);
            return res;
        }

        public async Task<List<Wallet>> GetWalletByUserId(Guid userId)
        {
            var res = await _walletRepo.GetWalletByUserId(userId);
            return res;
        }
    }
}
