using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserService.Domain.Models;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories
{
    public class UserRepository : IUserRepo
    {
        private readonly UserDbContext _dbContext;
        public UserRepository(UserDbContext userDbContext)
        {
            _dbContext = userDbContext;
        }
        public async Task<User> CreateUser(User user)
        {
            var res = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<User> GetUserBydId(Guid id)
        {
            var res = await _dbContext.Users.FindAsync(id);
            return res;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<string> LoginUser(string email, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Email == email);
            if (user == null || !VerifyPassword(password, user.Password)) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("your_very_secure_and_long_secret_key_32_chars");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Issuer = "UserService.Issuer",
                Audience = "UserService.Audience",
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            string hashedInput = HashPassword(password);
            return hashedInput.Equals(hashedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}
