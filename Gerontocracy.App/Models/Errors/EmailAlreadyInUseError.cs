namespace Gerontocracy.App.Models.Errors
{
    /// <summary>
    /// Describes an error, if the email address is already in use
    /// </summary>
    public class EmailAlreadyInUseError : Error
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EmailAlreadyInUseError() : base("0x00000002", "E-Mail already in use!", "E-Mail Address is already in use! Pick another one!") { }
    }
}
