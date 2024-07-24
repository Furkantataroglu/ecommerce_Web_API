using DAL_DataAccessLayer.EntityFramework.Mappings;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DataAccessLayer.EntityFramework.Contexts
{
    public class MyDbContext:DbContext
    {
        //Databaseleri setleme
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: @"Server=(localdb)\\mssqllocaldb;Database=NewDb;Trusted_Connection=True;MultipleActiveResultSets=True");
        }
        //burada mappingleri configure ediyoruz. veri tabanımız oluşurken bu maplerin özelliklerini alacak
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
           // modelBuilder.ApplyConfiguration(new CustomerMapping()); 
        }
    }
}
