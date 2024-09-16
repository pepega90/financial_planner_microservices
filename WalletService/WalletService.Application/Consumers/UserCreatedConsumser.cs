using MassTransit;
using Microsoft.Extensions.Logging;
using SharedService.Messaging.Messages;
using WalletService.Application.Interfaces;

namespace WalletService.Application.Consumers
{
    public class UserCreatedConsumer: IConsumer<UserCreatedMessage>
    {
        private readonly IWalletService _walletService;
        public readonly ILogger<UserCreatedConsumer> _logger;
        public UserCreatedConsumer(IWalletService walletService, ILogger<UserCreatedConsumer> logger)
        {
            _walletService = walletService;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<UserCreatedMessage> context)
        {
            var message = context.Message;

            _logger.LogInformation($"Consume data from user service = {message.UserId}, {message.Username}, {message.Email}");

            await _walletService.CreateDefaultWalletUser(message);
        }
    }
}
