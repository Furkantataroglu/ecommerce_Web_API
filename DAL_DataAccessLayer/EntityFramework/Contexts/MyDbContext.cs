using DAL_DataAccessLayer.EntityFramework.Mappings;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DataAccessLayer.EntityFramework.Contexts
{
    public class MyDbContext:IdentityDbContext<User>
    {
        //Databaseleri setleme
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(connectionString: @"Server=(localdb)\\mssqllocaldb;Database=NewDb;Trusted_Connection=True;MultipleActiveResultSets=True");
        //}
        //burada mappingleri configure ediyoruz. veri tabanımız oluşurken bu maplerin özelliklerini alacak
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensure Identity configurations are applied

            modelBuilder.Entity<IdentityUserClaim<int>>().HasKey(c => c.Id);


            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Users");
            });

            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new RoleMapping());
           // modelBuilder.ApplyConfiguration(new CustomerMapping()); 
        }
    }
}
