using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedService.Messaging.Messages
{
    public class GetWalletInfoMessage
    {
        public Guid UserId { get; set; }
    }
}
