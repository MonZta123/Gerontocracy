using System;
using System.ComponentModel.DataAnnotations;

namespace Gerontocracy.App.Models.Account
{
    /// <summary>
    /// Model for Login
    /// </summary>
    [Serializable]
    public class Login
    {
        /// <summary>
        /// Username
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// RememberMe Flag in Cookie
        /// </summary>
        [Required]
        public bool RememberMe { get; set; }
    }
}
