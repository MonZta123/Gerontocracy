using System;
using Gerontocracy.Core.Config;

namespace Gerontocracy.Core
{
    public class GerontocracyOptions
    {
        internal string ConnectionString { get; set; }

        internal GerontocracySettings GerontocracyConfig { get; set; } = new GerontocracySettings();

        public GerontocracyOptions UseNpgsql(string connectionString)
        {
            this.ConnectionString = connectionString;
            return this;
        }

        public GerontocracyOptions UseConfig(Action<GerontocracySettings> gerontocracyConfig)
        {
            gerontocracyConfig(this.GerontocracyConfig);
            return this;
        }
    }
}
