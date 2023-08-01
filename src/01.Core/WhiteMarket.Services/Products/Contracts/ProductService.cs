using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Services.Products.Contracts.Dto;

namespace WhiteMarket.Services.Products.Contracts
{
    public interface ProductService
    {
        void Define(AddProductDto dto);
    }
}
