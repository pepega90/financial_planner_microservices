using SharedService.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTOs;
using UserService.Domain.Models;

namespace UserService.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(CreateUserDto createUserDto);
        Task<IEnumerable<User>> GetUsers();
        Task<WalletInfoResponse> GetUserWallet(Guid id);
        Task<User> GetUserById(Guid id);
    }
}
