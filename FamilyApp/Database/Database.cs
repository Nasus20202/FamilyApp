using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FamilyApp
{
    public class Database : DbContext
    {
        public static string ConnectionString = "";
        public DbSet<User> Users { get; set; }

        public DbSet<Family> Families { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var serverVerison = new MySqlServerVersion(new Version(8, 0, 25));
            optionsBuilder.UseMySql(ConnectionString, serverVerison).EnableSensitiveDataLogging().EnableDetailedErrors();
        }
    }
}
