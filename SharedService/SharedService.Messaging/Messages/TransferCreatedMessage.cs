using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedService.Messaging.Messages
{
    public class TransferCreatedMessage
    {
        public Guid FromWalletId { get; set; }
        public Guid ToWalletId { get; set; }
        public double Amount { get; set; }
    }
}
