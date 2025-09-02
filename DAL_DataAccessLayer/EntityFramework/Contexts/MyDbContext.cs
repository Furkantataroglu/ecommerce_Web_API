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
    public class MyDbContext:IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        //Databaseleri setleme
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        //public DbSet<Role> Roles { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(connectionString: @"Server=(localdb)\\mssqllocaldb;Database=NewDb;Trusted_Connection=True;MultipleActiveResultSets=True");
        //}
        //burada mappingleri configure ediyoruz. veri tabanımız oluşurken bu maplerin özelliklerini alacak
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensure Identity configurations are applied

            //Rolleri Tanımlama
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(
                  new IdentityRole<Guid>
                  {
                      Id = new Guid("223f336d-92ca-47cc-8e93-44e0efd39112"),
                      Name = "Admin",
                      NormalizedName = "ADMIN"
                  },
                  new IdentityRole<Guid>
                  {
                      Id = new Guid("4b597848-ab45-4994-b4a4-7de1bceb2d6a"),
                      Name = "User",
                      NormalizedName = "USER"
                  }
              );
            //modelBuilder.Entity<Role>(entity =>
            //{
            //    entity.HasKey(e => e.Id);
            //    entity.ToTable("Roles");
            //});
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new CartMapping());
            modelBuilder.ApplyConfiguration(new CartItemMapping());
            modelBuilder.ApplyConfiguration(new ProductMapping());
            //modelBuilder.ApplyConfiguration(new RoleMapping());
           // modelBuilder.ApplyConfiguration(new CustomerMapping()); 
        }
    }
}
