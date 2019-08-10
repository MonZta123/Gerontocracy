using System;
using System.Collections.Generic;

namespace Gerontocracy.App.Models.Account
{
    /// <summary>
    /// Defines a user
    /// </summary>
    [Serializable]
    public class User
    {
        /// <summary>
        /// The users Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The users name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The users join date
        /// </summary>
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// User roles
        /// </summary>
        public IList<string> Roles { get; set; }
    }
}
