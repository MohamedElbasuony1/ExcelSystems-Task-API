using DAL.Configurations;
using Microsoft.EntityFrameworkCore;
using Models;
using System;

namespace DAL
{
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions options)
            :base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
