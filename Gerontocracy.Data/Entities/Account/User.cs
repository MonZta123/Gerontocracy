using System;
using System.Collections.Generic;
using Gerontocracy.Data.Entities.Affair;
using Microsoft.AspNetCore.Identity;

namespace Gerontocracy.Data.Entities.Account
{
    public partial class User : IdentityUser<long>
    {
        public User()
        {
            this.RegisterDate = DateTime.Now;
        }

        public DateTime RegisterDate { get; set; }

        public ICollection<Vorfall> Vorfaelle { get; set; }
    }
}
