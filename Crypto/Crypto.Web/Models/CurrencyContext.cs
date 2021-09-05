using Crypto.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Crypto.Web.Models
{
    public class CurrencyContext : DbContext
    {

        public DbSet<NewCurrencyModel> Currency { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           var machine =  System.Environment.MachineName;
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RR8E0R8\SQLEXPRESS;Database=CurrencyDB;Trusted_Connection=True;");
        }

    }
}
