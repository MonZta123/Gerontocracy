namespace Gerontocracy.App.Models.Errors
{
    /// <summary>
    /// Describes an Error if there's an invalid input
    /// </summary>
    public class CommandError : Error
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="description">Description of the error</param>
        public CommandError(string description) : base("0x00000007", "Input Error", description)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="description">Description of the error</param>
        /// <param name="title">Title of the error</param>
        public CommandError(string description, string title) : base("0x00000007", title, description)
        {
        }
    }
}
