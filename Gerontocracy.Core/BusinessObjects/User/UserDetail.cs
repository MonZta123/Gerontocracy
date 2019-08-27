using System;
using System.Collections.Generic;
using Gerontocracy.Core.BusinessObjects.Account;

namespace Gerontocracy.Core.BusinessObjects.User
{
    public class UserDetail
    {
        public long Id { get; set; }

        public  string UserName { get; set; }

        public DateTime RegisterDate { get; set; }

        public int VorfallCount { get; set; }

        public bool EmailConfirmed { get; set; }

        public int AccessFailedCount { get; set; }

        public bool Banned { get; set; }

        public DateTime? LockoutEnd { get; set; }

        public IEnumerable<Role> Roles { get; set; }
    }
}
