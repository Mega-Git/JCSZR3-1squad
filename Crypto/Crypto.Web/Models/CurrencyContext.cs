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
        public DbSet<NewCurrencyPricesModel> Price { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewCurrencyModel>()
                .HasMany(x => x.Prices)
                .WithOne(x => x.Currency)
                .HasForeignKey(x => x.CurrencyId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var machineName = Environment.MachineName;
            optionsBuilder.UseSqlServer(
                $@"Server={machineName}\SQLEXPRESS;Database=CurrencyDB;Trusted_Connection=True;");
        }

        public CurrencyContext()
        {
            
        }
    }
}