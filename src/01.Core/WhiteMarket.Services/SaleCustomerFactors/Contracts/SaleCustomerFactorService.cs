using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Services.SaleCustomerFactors.Contracts.Dto;

namespace WhiteMarket.Services.SaleCustomerFactors.Contracts
{
    public interface SaleCustomerFactorService
    {
        void Register(AddSaleCustomerFactorDto dto);
    }
}
