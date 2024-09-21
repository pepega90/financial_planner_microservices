namespace TransactionService.Grpc.Models
{
    public class Transfer
    {
        public Guid Id { get; set; }
        public Guid FromWalletId { get; set; }
        public Guid ToWalletId { get; set; }
        public double Amount { get; set; }
    }
}
