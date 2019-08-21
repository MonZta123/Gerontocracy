using System;

namespace Gerontocracy.App.Models.User
{
    /// <summary>
    /// Reflects the data of a user
    /// </summary>
    public class UserData
    {
        /// <summary>
        /// identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// registered on
        /// </summary>
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// score
        /// </summary>
        public int Score { get; set; }
    }
}
