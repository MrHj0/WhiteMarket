using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.Services.SaleCustomerFactors.Contracts;

namespace WhiteMarket.Presistence.EF.SaleCustomerFactors
{
    public class EFSaleCustomerFactorRepository : SaleCustomerFactorRepository
    {
        private readonly DbSet<SaleCustomerFactor> _customerFactors;

        public EFSaleCustomerFactorRepository(EFDataContext context)
        {
            _customerFactors = context.Set<SaleCustomerFactor>();
        }

        public void Add(SaleCustomerFactor factor)
        {
            _customerFactors.Add(factor);
        }
    }
}
