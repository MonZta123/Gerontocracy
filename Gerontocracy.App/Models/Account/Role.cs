using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerontocracy.App.Models.Account
{
    /// <summary>
    /// Role which is shown in the frontend
    /// </summary>
    public class Role
    {   
        /// <summary>
        /// Role id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        public string Name { get; set; }
    }
}
