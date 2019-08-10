using System.Collections.Generic;

namespace Gerontocracy.App.Models.Dashboard
{
    /// <summary>
    /// Contains the data shown on the dashboard
    /// </summary>
    public class DashboardData
    {
        /// <summary>
        /// Contains last 15 news entries
        /// </summary>
        public List<Artikel> News { get; set; }
    }
}
