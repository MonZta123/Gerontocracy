using System;
using System.Collections.Generic;
using Gerontocracy.App.Models.Account;

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

        /// <summary>
        /// Roles of user
        /// </summary>
        public IEnumerable<Role> Roles { get; set; }

        /// <summary>
        /// List of last 15 submitted affairs
        /// </summary>
        public IEnumerable<Vorfall> Affairs { get; set; }
        
        /// <summary>
        /// List of last 15 posts
        /// </summary>
        public IEnumerable<Post> Posts { get; set; }
    }
}
