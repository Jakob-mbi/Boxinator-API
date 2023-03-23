namespace Boxinator_API.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string sub) : base($"User with id {sub} was not found!")
        {

        }
    }
}
