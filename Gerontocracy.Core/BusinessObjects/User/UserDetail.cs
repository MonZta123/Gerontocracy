using System;
using System.Collections.Generic;

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

        public DateTimeOffset? LockoutEnd { get; set; }

        public IList<string> Roles { get; set; }
    }
}
