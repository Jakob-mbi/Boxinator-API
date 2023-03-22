using Boxinator_API.DTOs.CountryDTOs;
using Boxinator_API.Models;

namespace Boxinator_API.Services.CountriesDataAccess
{
    public interface ICountryService : ICrudRepository<Country, int>
    {
    }
}
