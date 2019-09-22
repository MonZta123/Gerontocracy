using System;

namespace Gerontocracy.App.Models.Account
{
    /// <summary>
    /// Reflects the data required for banning a user
    /// </summary>
    public class BanData
    {
        /// <summary>
        /// Id of user banned
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Reason why user has to banned
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Duration of ban
        /// </summary>
        public TimeSpan? Duration { get; set; }
    }
}
