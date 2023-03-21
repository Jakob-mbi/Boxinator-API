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

        public async Task<Country> Update(PutCountryDTO countryDTO)
        {
            var country = await _context.Countries.FindAsync(countryDTO.Id);


            if (country == null)
            {
                throw new CountryNotFoundException(countryDTO.Id);
            }

            try
            {
                country.Name = countryDTO.Name;
                country.Multiplier = countryDTO.Multiplier;
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
