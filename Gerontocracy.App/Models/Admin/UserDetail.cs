using System;
using System.Collections.Generic;

namespace Gerontocracy.App.Models.Admin
{
    /// <summary>
    /// Descriptiuon of detailed user object
    /// </summary>
    public class UserDetail
    {
        /// <summary>
        /// User id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Represents the users name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Represents the date, when user was registered
        /// </summary>
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// How many affairs did he submit
        /// </summary>
        public int VorfallCount { get; set; }

        /// <summary>
        /// Shows, whether his eMail adress was confirmed
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Shows how often the access failed
        /// </summary>
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// When is the lockout over?
        /// </summary>
        public DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// The users roles
        /// </summary>
        public IList<string> Roles { get; set; }
    }
}
