using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.Services.SaleAccountingFactors.Contracts;

namespace WhiteMarket.Presistence.EF.SaleAccountingFactors
{
    public class EFSaleAccountingFactorRepository : SaleAccountingFactorRepository
    {
        private readonly DbSet<SaleAccountingFactor> _accountingFactors;

        public EFSaleAccountingFactorRepository(EFDataContext context)
        {
            _accountingFactors = context.Set<SaleAccountingFactor>();
        }

        public void Add(SaleAccountingFactor factor)
        {
            _accountingFactors.Add(factor);
        }
    }
}
