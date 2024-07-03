using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitualReception.Domain.Model
{
    internal class DbManager : DbContext
    {
        public DbSet<Chat> Chat { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost;database=chats;user=root;password=FnafAr12!";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        }
    }
}
