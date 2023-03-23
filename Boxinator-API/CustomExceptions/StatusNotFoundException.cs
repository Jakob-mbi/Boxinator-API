namespace Boxinator_API.CustomExceptions
{
    public class StatusNotFoundException : Exception
    {
        public StatusNotFoundException() : base("No status was not found")
        {

        }
    }
}
