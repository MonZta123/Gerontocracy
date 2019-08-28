namespace Gerontocracy.App.Models.Account
{
    /// <summary>
    /// Reflects the dataset required for unbanning a user
    /// </summary>
    public class UnbanData
    {
        /// <summary>
        /// Id of user to unban
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Reason why user is unbanned
        /// </summary>
        public string Reason { get; set; }
    }
}
