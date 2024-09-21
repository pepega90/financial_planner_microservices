using MassTransit;
using Microsoft.Extensions.Logging;
using SharedService.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletService.Application.Interfaces;

namespace WalletService.Application.Consumers
{
    public class TransferConsumer : IConsumer<TransferCreatedMessage>
    {
        private readonly IWalletService _walletService;
        public readonly ILogger<TransferConsumer> _logger;
        public TransferConsumer(IWalletService walletService, ILogger<TransferConsumer> logger)
        {
            _walletService = walletService;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<TransferCreatedMessage> context)
        {
            var message = context.Message;

            _logger.LogInformation($"Consume data from transfer service (gRPC) = {message.FromWalletId}, {message.ToWalletId}, {message.Amount}");

            await _walletService.TransferBetweenWallet(message.FromWalletId, message.ToWalletId, message.Amount);
        }
    }
}
