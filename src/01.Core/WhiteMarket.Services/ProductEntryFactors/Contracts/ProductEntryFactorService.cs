using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Services.ProductEntryFactors.Contracts.Dto;

namespace WhiteMarket.Services.ProductEntryFactors.Contracts
{
    public interface ProductEntryFactorService
    {
        void Register(AddProductEntryFactorDto dto);
    }
}
