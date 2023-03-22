namespace Boxinator_API.CustomExceptions
{
    public class CountryNotFoundException : Exception
    {
        public CountryNotFoundException(int id) : base($"Country with id {id} not found")
        {
        }
    }
}
