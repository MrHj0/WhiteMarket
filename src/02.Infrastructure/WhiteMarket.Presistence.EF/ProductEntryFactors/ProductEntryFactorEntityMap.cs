using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;

namespace WhiteMarket.Presistence.EF.ProductEntryFactors
{
    public class ProductEntryFactorEntityMap : IEntityTypeConfiguration<ProductEntryFactor>
    {
        public void Configure(EntityTypeBuilder<ProductEntryFactor> entryFactor)
        {
            entryFactor.ToTable("ProductEntryFactors");

            entryFactor.HasKey(_ => _.PrimaryKey);

            entryFactor.Property(_=>_.PrimaryKey).ValueGeneratedOnAdd();
            entryFactor.Property(_ => _.Id).IsRequired();
            entryFactor.Property(_ => _.ProductId).IsRequired();
            entryFactor.Property(_ => _.ProductEntryCount).IsRequired();
            entryFactor.Property(_ => _.ProductName).IsRequired().HasMaxLength(50);
            entryFactor.Property(_ => _.CompanyName).IsRequired().HasMaxLength(50);
            entryFactor.Property(_ => _.Date).IsRequired();
        }
    }
}
