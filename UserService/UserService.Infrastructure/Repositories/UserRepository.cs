using Microsoft.EntityFrameworkCore;
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
    }
}
