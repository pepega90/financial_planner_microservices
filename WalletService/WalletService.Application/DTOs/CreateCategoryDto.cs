using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WalletService.Application.DTOs
{
    public class CreateCategoryDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;
    }
}
