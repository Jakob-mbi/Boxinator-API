using Boxinator_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Boxinator_API.Services
{
    public interface IUserService
    {
        Task<ActionResult<User>> AddUser(string sub, User user, int roleId, string email);
        Task DeleteUser(int id);
        Task<IEnumerable<User>> GetAllUsers();
      //  Task<User> GetUserById(int id);
        Task<User> GetUserBySub(string sub);
        Task<User> UpdateUser(User user, string sub);
    }
}
