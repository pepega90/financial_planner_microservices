using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedService.Messaging.Messages
{
    public class WalletInfoResponse
    {
        public List<WalletDto> Wallets { get; set; } = new List<WalletDto>();
    }

    public class WalletDto
    {
        public string Name { get; set; } = default!;
        public decimal Balance { get; set; }
        public List<Record> Records { get; set; } = new List<Record>();
    }

    public class Record
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Tanggal { get; set; }
        public string Type { get; set; }
        public Category Category { get; set; } 
    }

    public class Category
    {
        public string Name { get; set; } = default!;
    }

    public enum TransactionType
    {
        Income,
        Expense
    }
}
