namespace Gerontocracy.App.Models.Errors
{
    /// <summary>
    /// Describes an error if the email address was already confirmed
    /// </summary>
    public class EmailAlreadyConfirmedError : Error
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EmailAlreadyConfirmedError() : base("0x00000001", "E-Mail-Address already confirmed!", "E-Mail-Address was already confirmed! You can already log in!")
        {
        }
    }
}
