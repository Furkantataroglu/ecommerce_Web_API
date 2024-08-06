//using Entities.Concrete;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DAL_DataAccessLayer.EntityFramework.Mappings
//{
//    public class RoleMapping : IEntityTypeConfiguration<Role>
//    {
//        public void Configure(EntityTypeBuilder<Role> builder)
//        {
//            builder.HasKey(x => x.Id);
//            builder.Property(a => a.Id).ValueGeneratedOnAdd();
//            builder.ToTable("Roles");
//        }
//    }
//}
