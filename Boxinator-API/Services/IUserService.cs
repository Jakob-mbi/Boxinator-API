using Boxinator_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Boxinator_API.Services
{
    public interface IUserService
    {
        Task<ActionResult<User>> AddUser(User user);
        Task DeleteUser(int id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> GetUserBySub(string sub);
        Task<User> UpdateUser(User user);
    }
}
