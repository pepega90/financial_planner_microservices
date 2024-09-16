using MassTransit;
using Microsoft.Extensions.Logging;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Application.Utils;
using SharedService.Messaging.Messages;
using UserService.Domain.Models;

namespace UserService.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<UserService> _logger;
        private readonly IRequestClient<GetWalletInfoMessage> _client;
        public UserService(IUserRepo userRepo, ILogger<UserService> logger, IPublishEndpoint publishEndpoint,
            IRequestClient<GetWalletInfoMessage> client)
        {
            _userRepo = userRepo;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _client = client;
        }
        public async Task<User> CreateUser(CreateUserDto createUserDto)
        {
            var hashedPassword = PasswordHash.HashPassword(createUserDto.Password);

            var createdUser = new User()
            {
                Email = createUserDto.Email,
                Password = hashedPassword,
                Username = createUserDto.Username,
            };
            var res = await _userRepo.CreateUser(createdUser);

            _logger.LogInformation($"Publish data from user service = {createdUser.Id}, {createdUser.Username}, {createdUser.Email}");

            await _publishEndpoint.Publish(new UserCreatedMessage
            {
                UserId = res.Id,
                Email = res.Email,
                Username = res.Username
            });

            return res;
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _userRepo.GetUserBydId(id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepo.GetUsers();
        }

        public async Task<WalletInfoResponse> GetUserWallet(Guid id)
        {
            var res = await _client.GetResponse<WalletInfoResponse>(new GetWalletInfoMessage { UserId = id });
            return res.Message;
        }
    }
}
