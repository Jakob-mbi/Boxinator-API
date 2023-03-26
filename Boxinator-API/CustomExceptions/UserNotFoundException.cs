namespace Boxinator_API.CustomExceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string sub) : base($"User with id {sub} was not found!")
        {

        }
    }
}
