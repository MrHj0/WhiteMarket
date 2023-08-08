using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;

namespace WhiteMarket.Services.ProductEntryFactors.Contracts
{
    public interface ProductEntryFactorRepository
    {
        void Add(ProductEntryFactor factor);
    }
}
