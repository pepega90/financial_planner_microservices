using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WalletService.Application.DTOs
{
    public class CreateWalletDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }
    }
}
