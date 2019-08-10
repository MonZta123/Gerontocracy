namespace Gerontocracy.App.Models.Errors
{
    /// <summary>
    /// Describes an error if the user wasn't found
    /// </summary>
    public class UserNotFoundError : Error
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserNotFoundError() : base("0x00000005", "Username not found", "The requested username wasn't found in the database!" ) { }
    }
}
