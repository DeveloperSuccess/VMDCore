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

        public DbSet<Drink> Drinks { get; set; }
    }
}
