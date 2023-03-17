using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Boxinator_API.Models;
using Boxinator_API.Services.CountriesDataAccess;
using Boxinator_API.DTOs.CountryDTOs;
using Boxinator_API.CustomExceptions;

namespace Boxinator_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        /// <summary>
        /// Get a list of all countries
        /// </summary>
        /// <returns>List of countries</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDTO>>> GetCountries()
        {

            var countries = await _countryService.ReadAll();

            var countryDTOs = countries.Select(country => new GetCountryDTO
            {
                Id = country.Id,
                Name = country.Name,
                Multiplier = country.Multiplier
            });

            return Ok(countryDTOs);
        }

        /// <summary>
        /// Get a country by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCountryDTO>> GetCountry(int id)
        {
            try
            {
                var country = await _countryService.ReadById(id);

                var countryDTO = new GetCountryDTO
                {
                    Id = country.Id,
                    Name = country.Name,
                    Multiplier = country.Multiplier
                };

                return Ok(countryDTO);
            }
            catch (CountryNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }

        //// PUT: api/Countries/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCountry(int id, Country country)
        //{
        //    if (id != country.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(country).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CountryExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Countries
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Country>> PostCountry(Country country)
        //{
        //  if (_context.Countries == null)
        //  {
        //      return Problem("Entity set 'BoxinatorDbContext.Countries'  is null.");
        //  }
        //    _context.Countries.Add(country);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        //}

        //// DELETE: api/Countries/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCountry(int id)
        //{
        //    if (_context.Countries == null)
        //    {
        //        return NotFound();
        //    }
        //    var country = await _context.Countries.FindAsync(id);
        //    if (country == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Countries.Remove(country);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool CountryExists(int id)
        //{
        //    return (_context.Countries?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
