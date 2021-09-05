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
          
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Currency;Integrated Security=True;Connect Timeout=30;");
        }

    }
}
