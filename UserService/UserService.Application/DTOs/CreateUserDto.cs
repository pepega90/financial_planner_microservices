using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserService.Application.DTOs
{
    public class CreateUserDto
    {
        [JsonPropertyName("username")]
        public string Username { get; set; } = default!;
        [JsonPropertyName("email")]
        public string Email { get; set; } = default!;
        [JsonPropertyName("password")]
        public string Password { get; set; } = default!;
    }
}
