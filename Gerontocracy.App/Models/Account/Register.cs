using System;
using System.ComponentModel.DataAnnotations;

namespace Gerontocracy.App.Models.Account
{
    /// <summary>
    /// Registermodel
    /// </summary>
    [Serializable]
    public class Register
    {
        /// <summary>
        /// User-Email
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

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
    }
}
