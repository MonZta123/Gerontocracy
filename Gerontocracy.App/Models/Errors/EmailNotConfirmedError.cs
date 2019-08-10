namespace Gerontocracy.App.Models.Errors
{
    /// <summary>
    /// Describes an error 
    /// </summary>
    public class EmailNotConfirmedError : Error
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EmailNotConfirmedError() : base("0x00000003", "E-Mail not confirmed!", "The entered username's assigned E-Mail-Address is not confirmed. Also Check your spamfolder!")
        {
        }
    }
}
