using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletService.Domain.Models
{
    public class Wallet
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Balance { get; set; }
        public Guid UserId { get; set; }
        public List<Record> Records { get; set; } = new List<Record>();
    }

    public interface IWalletRepo
    {
        Task<Wallet> CreateWallet(Wallet wallet);
        Task<List<Wallet>> GetWalletByUserId(Guid userId);
    }
}
