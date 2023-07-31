using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;

namespace WhiteMarket.Presistence.EF.SaleAccountingFactors
{
    public class SaleAccountingFactorEntityMap : IEntityTypeConfiguration<SaleAccountingFactor>
    {
        public void Configure(EntityTypeBuilder<SaleAccountingFactor> accountingFactor)
        {
            accountingFactor.ToTable("SaleAccountingFactors");

            accountingFactor.HasKey(_ => _.Id);

            accountingFactor.Property(_=>_.Id).IsRequired();
            accountingFactor.Property(_=>_.TotalPrice).IsRequired();
            accountingFactor.Property(_=>_.CustomerFactorId).IsRequired();
            accountingFactor.Property(_=>_.Date).IsRequired();

        }
    }
}
