using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Services.ProductEntryFactors.Contracts.Dto;

namespace WhiteMarket.TestTools.ProductEntryFactors
{
    public static class AddProductEntryFactorDtoFactory
    {
        public static AddProductEntryFactorDto Generate(int productId)
        {
            return new AddProductEntryFactorDto
            {
                CompanyName = "dummy_company",
                Count = 10,
                FactorId = "123a",
                ProductId = productId
            };
        }
    }
}
