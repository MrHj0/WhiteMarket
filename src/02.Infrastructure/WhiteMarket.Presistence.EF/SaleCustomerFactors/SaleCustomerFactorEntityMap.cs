using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;

namespace WhiteMarket.Presistence.EF.SaleCustomerFactors
{
    public class SaleCustomerFactorEntityMap : IEntityTypeConfiguration<SaleCustomerFactor>
    {
        public void Configure(EntityTypeBuilder<SaleCustomerFactor> customerFactor)
        {
            customerFactor.ToTable("SaleCustomerFactors");

            customerFactor.HasKey(_ => _.DummyPrimaryKey);

            customerFactor.Property(_ => _.DummyPrimaryKey).ValueGeneratedNever();
            customerFactor.Property(_ => _.Id).IsRequired();
            customerFactor.Property(_ => _.ProductId).IsRequired();
            customerFactor.Property(_ => _.ProductName).IsRequired().HasMaxLength(50);
            customerFactor.Property(_ => _.Price).IsRequired();
            customerFactor.Property(_ => _.Count).IsRequired();
            customerFactor.Property(_ => _.CustomerName).IsRequired().HasMaxLength(50);
            customerFactor.Property(_ => _.Date).IsRequired();
        }
    }
}
