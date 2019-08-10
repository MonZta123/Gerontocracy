namespace Gerontocracy.App.Models.Errors
{
    /// <summary>
    /// Describes an error, if the password is wrong
    /// </summary>
    public class WrongPasswordError : Error
    {
        /// <summary>
        /// Describes an error if the password is wrong
        /// </summary>
        public WrongPasswordError() : base("0x00000006", "Wrong Password", "The Password you entered was wrong") { }
    }
}
