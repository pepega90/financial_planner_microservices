using Grpc.Core;
using MassTransit;
using TransactionService.Protos;

namespace TransactionService.Grpc.Services
{
    public class TransactionService : Protos.TransactionService.TransactionServiceBase
    {
        public readonly IPublishEndpoint _publishEndpoint;
        public TransactionService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public override Task<TransferResponse> Transfer(TransferRequest request, ServerCallContext context)
        {
            return base.Transfer(request, context);
        }
    }
}
