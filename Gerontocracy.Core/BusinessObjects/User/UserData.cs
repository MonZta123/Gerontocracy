using System;
using System.Collections.Generic;
using Gerontocracy.Core.BusinessObjects.Account;

namespace Gerontocracy.Core.BusinessObjects.User
{
    public class UserData
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime RegisterDate { get; set; }

        public int Score { get; set; }

        public bool Banned { get; set; }

        public DateTime? LockoutEnd { get; set; }

        public IEnumerable<Role> Roles { get; set; }

        public IEnumerable<Vorfall> Affairs { get; set; }

        public IEnumerable<Thread> Threads { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}
