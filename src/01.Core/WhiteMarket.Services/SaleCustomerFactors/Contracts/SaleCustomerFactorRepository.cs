using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;

namespace WhiteMarket.Services.SaleCustomerFactors.Contracts
{
    public interface SaleCustomerFactorRepository
    {
        void Add(SaleCustomerFactor factor);
    }
}
