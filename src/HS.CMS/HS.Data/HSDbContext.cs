using HS.Data.Entities;
using HS.IService.Menus;
using HS.IService.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HS.Data
{
    public class HSDbContext:DbContext
    {
        public  HSDbContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Menu>().ToTable("Menus");
        }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
