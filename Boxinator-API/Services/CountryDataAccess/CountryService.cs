using Boxinator_API.CustomExceptions;
using Boxinator_API.DTOs.CountryDTOs;
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

        public async Task<Country> Create(Country country)
        {
            //_context.Countries.Add(country);
            //await _context.SaveChangesAsync();

            //return country;
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

        public async Task<Country> Update(Country country)
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // In case of concurrency conflict, reload the entity from the database and try again
                ex.Entries.Single().Reload();
                await _context.SaveChangesAsync();
            }

            return country;
        }
    }
}
