using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VMDCore.Data.Models;

namespace VMDCore.Data
{
    public class VmdDbContext : DbContext
    {
        public VmdDbContext(DbContextOptions<VmdDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Operation>().HasData
              (
                  new Operation() { OperationId = 1, Balance=0, Message="" }
              );
        }

        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Operation> Operations { get; set; }
    }
}
