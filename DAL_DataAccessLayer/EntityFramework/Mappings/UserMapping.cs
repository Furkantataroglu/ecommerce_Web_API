using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DataAccessLayer.EntityFramework.Mappings
{
    public class UserMapping:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(a => a.Id); //PK yapmak için
            builder.Property(a=>a.Id).ValueGeneratedOnAdd(); //eklendikçe 1 1 id artıyor.
            //builder.Property(a =>a.FirstName).HasMaxLength(50); //gibi özellikler
            //builder.Property(a => a.FirstName).IsRequired(true);
            //builder.Property(a => a.LastName).IsRequired(true);
            //builder.Property(a => a.Password).IsRequired(true);
            //foreign key one to many her kişinin bir rolü var her rolün birden fazla kişisi olabilir
            //builder.HasOne<Role>(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);

            builder.ToTable("Users"); //tablonun adı 
        }
    }
}
