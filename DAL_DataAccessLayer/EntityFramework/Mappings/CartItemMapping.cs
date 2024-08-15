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
    public class CartItemMapping : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(a => a.Id); //PK yapmak için
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            // Relationship with Product
            builder.HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .IsRequired();

            // Relationship with Cart
            builder.HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .IsRequired();
            builder.ToTable("CartItem");
        }
    }
}
