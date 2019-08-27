using System;
using System.Collections.Generic;
using Gerontocracy.App.Models.Account;

namespace Gerontocracy.App.Models.Admin
{
    /// <summary>
    /// Description of detailed user object
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
        /// User is banned
        /// </summary>
        public bool Banned { get; set; }

        /// <summary>
        /// When is the lockout over?
        /// </summary>
        public DateTime? LockoutEnd { get; set; }

        /// <summary>
        /// The users roles
        /// </summary>
        public IEnumerable<Role> Roles { get; set; }
    }
}
