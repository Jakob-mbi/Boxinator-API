using Boxinator_API.Exceptions;
using Boxinator_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Boxinator_API.Services
{
    public class UserService : IUserService
    {
        public readonly BoxinatorDbContext _context;

        public UserService(BoxinatorDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<User>> AddUser(User user)
        {
            var existingUser = await _context.Users
                .SingleOrDefaultAsync(u => u.Sub == user.Sub);
            if (existingUser == null)
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }

            return user;
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);  
            if (user == null)
            {
                throw new UserNotFoundException(id);
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.Include(x => x.ShipmentsList).ToListAsync();
        }

        public Task<User> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        /*public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.Include(x => x.ShipmentsList).FirstOrDefaultAsync(x => x.Id == id);
            if(user is null)
            {
                throw new UserNotFoundException(id);
            }
            return user;
        }
*/
        public async Task<User> GetUserBySub(string sub)
        {
            var user = await _context.Users.Include(x => x.ShipmentsList).FirstOrDefaultAsync(x => x.Sub == sub);
            if(user is null)
            {
                throw new Exception();
            }

            return user;
        }

        public Task<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        /*public async Task<User> UpdateUser(User user)
        {
            var existingUser = await _context.Users
                .SingleOrDefaultAsync(u => u.Sub == user.Sub);
            if (existingUser is null)
            {
                throw new UserNotFoundException(user.Id);
            }
            existingUser.DateOfBirth = user.DateOfBirth;
            existingUser.Country = user.Country;
            existingUser.ZipCode = user.ZipCode;
            existingUser.ContactNumber = user.ContactNumber;

            await _context.SaveChangesAsync();

            return existingUser;
        }*/
    }
}
