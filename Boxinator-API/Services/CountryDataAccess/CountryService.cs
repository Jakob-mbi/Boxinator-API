using Boxinator_API.CustomExceptions;
using Boxinator_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Boxinator_API.Services.CountriesDataAccess
{
    public class CountryService : ICountryService
    {
        private readonly BoxinatorDbContext _context;

        public CountryService(BoxinatorDbContext context)
        {
            _context = context;
        }

        public Task<Country> Create(Country obj)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Country>> ReadAll()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country> ReadById(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                throw new CountryNotFoundException(id);
            }

            return country;
        }

        public Task<Country> Update(Country obj)
        {
            throw new NotImplementedException();
        }
    }
}
