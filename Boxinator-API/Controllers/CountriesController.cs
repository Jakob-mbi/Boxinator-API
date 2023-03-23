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
using Microsoft.AspNetCore.Cors;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Boxinator_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
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
                    Detail = ex.Message,
                });
            }
        }

        /// <summary>
        /// Update a country 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCountry(int id, [FromBody] PutCountryDTO country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }

            try
            {
                var countryToUpdate = await _countryService.ReadById(id);

                if (countryToUpdate == null)
                {
                    throw new CountryNotFoundException(id);
                }

                // Update fields
                countryToUpdate.Name = country.Name;
                countryToUpdate.Multiplier = country.Multiplier;

                // Save changes
                await _countryService.Update(countryToUpdate);
            }
            catch (CountryNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                { 
                    Detail = ex.Message,
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                // If the concurrency conflict can't be resolved in the service. Return status 409 (conflict)
                return Conflict(new ProblemDetails
                { 
                    Detail = "Concurrency conflict occurred while saving changes. Please refresh and try again." ,
                });
            }

            return Ok();
        }

        ///// <summary>
        ///// Create a new country in database
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="country"></param>
        ///// <returns></returns>
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost("{id}")]
        //public async Task<ActionResult<Country>> PostCountry([FromBody] PostCountryDTO country)
        //{
        //    //var country = new Country
        //    //{
        //    //    Name = countryDTO.Name,
        //    //    Multiplier = countryDTO.Multiplier
        //    //};

        //    var newCountry = await _countryService.Create(country);

        //    return CreatedAtAction("GetCountry", new { id = newCountry.Id }, newCountry);
        //}
    }
}
