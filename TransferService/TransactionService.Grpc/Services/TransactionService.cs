using Grpc.Core;
using MassTransit;
using SharedService.Messaging.Messages;
using TransactionService.Grpc.Data;
using TransactionService.Grpc.Models;
using TransactionService.Protos;

namespace TransactionService.Grpc.Services
{
    public class TransactionService : Protos.TransactionService.TransactionServiceBase
    {
        public TransDbContext _dbContext { get; set; }
        public readonly IPublishEndpoint _publishEndpoint;
        public TransactionService(TransDbContext dbContext,IPublishEndpoint publishEndpoint)
        {
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
        }
        public override async Task<TransferResponse> Transfer(TransferRequest request, ServerCallContext context)
        {
            if (string.IsNullOrEmpty(request.FromWalletId) || string.IsNullOrEmpty(request.ToWalletId) || request.Amount < 0)
            {
                return new TransferResponse
                {
                    Success = false,
                    Message = "Invalid transfer request",
                    TransactionId = string.Empty,
                };
            }
            var transferModel = new Transfer
            {
                FromWalletId = Guid.Parse(request.FromWalletId),
                ToWalletId = Guid.Parse(request.ToWalletId),
                Amount = request.Amount,
            };
            
           var res = _dbContext.Transfers.Add(transferModel);
            await _dbContext.SaveChangesAsync();

            var msgTransfer = new TransferCreatedMessage
            {
                Amount = transferModel.Amount,
                FromWalletId = transferModel.FromWalletId,
                ToWalletId = transferModel.ToWalletId,
            };

            await _publishEndpoint.Publish(msgTransfer);

            return new TransferResponse
            {
                Success = true,
                Message = $"Transfer from wallet id {request.FromWalletId} to {request.ToWalletId} is success!",
                TransactionId = res.Entity.Id.ToString(),
            };
        }
    }
}
