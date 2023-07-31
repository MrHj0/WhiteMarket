using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;

namespace WhiteMarket.Presistence.EF.Products
{
    public class ProductEntityMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> product)
        {
            product.ToTable("Products");

            product.HasKey(_ => _.Id);
            product.Property(_ => _.Id).ValueGeneratedOnAdd();
            product.Property(_ => _.Title).IsRequired().HasMaxLength(50);
            product.Property(_ => _.Status).IsRequired();
            product.Property(_=>_.Inventory).IsRequired();
            product.Property(_=>_.MinimumInventory).IsRequired();
            product.Property(_ => _.GroupId).IsRequired();

            product.HasOne(_=>_.Group)
                .WithMany(_ => _.Products)
                .HasForeignKey(_=>_.GroupId)
                .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
