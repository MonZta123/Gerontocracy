namespace Gerontocracy.App.Models.Errors
{
    /// <summary>
    /// Describes an error if the username is already in use
    /// </summary>
    public class UsernameAlreadyInUseError : Error
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UsernameAlreadyInUseError() : base("0x00000004", "Username already in use!", "The picked username is already in use. Pick another one.")
        {
        }
    }
}
