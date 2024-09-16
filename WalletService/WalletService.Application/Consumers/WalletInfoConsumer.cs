using MassTransit;
using Microsoft.Extensions.Logging;
using SharedService.Messaging.Messages;
using WalletService.Application.Interfaces;

namespace WalletService.Application.Consumers
{
    public class WalletInfoConsumer : IConsumer<GetWalletInfoMessage>
    {
        private readonly IWalletService _walletService;
        public readonly ILogger<WalletInfoConsumer> _logger;

        public WalletInfoConsumer(IWalletService walletService, ILogger<WalletInfoConsumer> logger)
        {
            _walletService = walletService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<GetWalletInfoMessage> context)
        {
            var message = context.Message;

            _logger.LogInformation($"Consume data from user service = {message.UserId}");
            var wallets = await _walletService.GetWalletByUserId(message.UserId);

            var response = new WalletInfoResponse
            {
                Wallets = wallets.Select(wallet => new WalletDto
                {
                    Balance = wallet.Balance,
                    Name = wallet.Name,
                    Records = MapRecords(wallet.Records)
                }).ToList()
            };

            await context.RespondAsync(response);
        }

        private List<SharedService.Messaging.Messages.Record> MapRecords(List<WalletService.Domain.Models.Record> domainRecords)
        {
            return domainRecords.Select(r => new SharedService.Messaging.Messages.Record
            {
                Id = r.Id,
                Amount = r.Amount,
                Category = r.Category != null ? new Category { Name = r.Category.Name } : null,
                Tanggal = r.Tanggal,
                Type = r.Type == Domain.Models.TransactionType.Income ? "Income" : "Expense"
            }).ToList();
        }
    }
}
