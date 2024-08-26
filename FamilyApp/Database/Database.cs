using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FamilyApp
{
    public class Database : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Family> Families { get; set; }

        public DbSet<ToDo> ToDos { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Message> Messages { get; set; }

        public string DbPath { get; set; }

        public Database()
        {
            var localPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "data");
            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }
            DbPath = Path.Join(localPath, "family.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }
}
