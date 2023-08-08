using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.Services.ProductEntryFactors.Contracts;

namespace WhiteMarket.Presistence.EF.ProductEntryFactors
{
    public class EFProductEntryFactorRepository : ProductEntryFactorRepository
    {
        private readonly DbSet<ProductEntryFactor> _productEntryFactors;
        public EFProductEntryFactorRepository(EFDataContext context)
        {
            _productEntryFactors = context.Set<ProductEntryFactor>();
        }

        public void Add(ProductEntryFactor factor)
        {
            _productEntryFactors.Add(factor);
        }
    }
}
