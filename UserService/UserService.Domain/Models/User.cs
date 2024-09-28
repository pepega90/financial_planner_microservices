namespace UserService.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

    public interface IUserRepo
    {
        Task<User> CreateUser(User user);
        Task<User> GetUserBydId(Guid id);
        Task<IEnumerable<User>> GetUsers();
        Task<string> LoginUser(string email, string password);
    }
}
