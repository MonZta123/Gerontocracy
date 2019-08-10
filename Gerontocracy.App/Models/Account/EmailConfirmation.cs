using System;

namespace Gerontocracy.App.Models.Account
{
    /// <summary>
    /// Defines the information needed for an email confirmation
    /// </summary>
    [Serializable]
    public class EmailConfirmation
    {
        /// <summary>
        /// The users id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The users generated token
        /// </summary>
        public string Token { get; set; }
    }
}
