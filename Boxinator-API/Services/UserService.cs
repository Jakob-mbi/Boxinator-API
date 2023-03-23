using Boxinator_API.Exceptions;
using Boxinator_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace Boxinator_API.Services
{
    public class UserService : IUserService
    {
        public readonly BoxinatorDbContext _context;

        public UserService(BoxinatorDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<User>> AddUser(string sub, User user, int roleId, string email)
        {
           

            var existingUser = await _context.Users
                .SingleOrDefaultAsync(u => u.Sub == sub);
          
            if (existingUser == null)
            {
                user.Sub = sub;
                user.DateOfBirth= null;
                user.ZipCode= null;
                user.Country = null;
                user.ContactNumber = null;
                user.RoleId = roleId;
              
                await _context.Users.AddAsync(user);
            }
            var existingShipments = await _context.Shipments
          .Where(s => s.Email == email)
          .ToListAsync();

            if (existingShipments.Any())
            {
                // Add the shipments to the user's shipmentList
                if (user.ShipmentsList == null)
                {
                    user.ShipmentsList = new List<Shipment>();
                }

                user.ShipmentsList.AddRange(existingShipments);
            }

            foreach (var shipment in existingShipments)
            {
                Console.WriteLine(shipment);
            }

            await _context.SaveChangesAsync();


            return user;
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);  
            if (user == null)
            {
                //throw new UserNotFoundException(id);
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.Include(x => x.ShipmentsList).ToListAsync();
        }

       /* public Task<User> GetUserById(int id)
        {
            throw new NotImplementedException();
        }*/

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


        public async Task<User> UpdateUser(User user, string sub)
        {
            var existingUser = await _context.Users
                .SingleOrDefaultAsync(u => u.Sub == sub);
            if (existingUser is null)
            {
                throw new UserNotFoundException(user.Sub);
            }
            existingUser.DateOfBirth = user.DateOfBirth;
            existingUser.Country = user.Country;
            existingUser.ZipCode = user.ZipCode;
            existingUser.ContactNumber = user.ContactNumber;

            await _context.SaveChangesAsync();

            return existingUser;
        }
    }
}
