
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Gerontocracy.Data.Entities.Party;
using Gerontocracy.Data.Entities.Affair;
using Gerontocracy.Data.Entities.Account;
using Gerontocracy.Data.Entities.Board;
using Gerontocracy.Data.Entities.News;
using System.Data.Common;
using System;
using System.Collections.Generic;

namespace Gerontocracy.Data
{
    public partial class GerontocracyContext : IdentityDbContext<User, Role, long>
    {
        public GerontocracyContext(DbContextOptions<GerontocracyContext> options) : base(options)
        {
        }

        public DbSet<Partei> Partei { get; set; }
        public DbSet<Politiker> Politiker { get; set; }

        public DbSet<Vorfall> Vorfall { get; set; }
        public DbSet<Vote> Vote { get; set; }
        public DbSet<Quelle> Quelle { get; set; }

        public DbSet<Post> Post { get; set; }
        public DbSet<Thread> Thread { get; set; }
        public DbSet<Like> Like { get; set; }

        public DbSet<Artikel> Artikel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);


        public List<T> GetData<T>(string query, Func<DbDataReader, T> readerFunc, Array parameters = null)
        {
            var data = new List<T>();
            using (var connection = this.Database.GetDbConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                connection.Open();
                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                        data.Add(readerFunc(result));
                }
                connection.Close();
            }

            return data;
        }
    }
}