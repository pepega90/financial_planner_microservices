using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WalletService.Domain.Models;

namespace WalletService.Application.DTOs
{
    public class CreateRecordDto
    {
        [JsonPropertyName("wallet_id")]
        public Guid WalletId { get; set; }
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("category_id")]
        public int CategoryId { get; set; }
    }
}
