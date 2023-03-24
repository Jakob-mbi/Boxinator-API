namespace Boxinator_API.CustomExceptions
{
    public class CountryNotFoundException : Exception
    {
        public CountryNotFoundException() : base("Country was not found")
        {

        }
    }
}
