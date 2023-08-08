using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Services.ProductEntryFactors.Contracts.Dto;

namespace WhiteMarket.TestTools.ProductEntryFactors
{
    public class AddProductEntryFactorDtoBuilder
    {
        private readonly AddProductEntryFactorDto _factor;

        public AddProductEntryFactorDtoBuilder(int productId)
        {
            _factor = new AddProductEntryFactorDto
            {
                CompanyName = "dummy_company",
                Count = 10,
                FactorId = "dummy_factor_id",
                ProductId = productId
            };
        }

        public AddProductEntryFactorDtoBuilder WithCount(int count)
        {
            _factor.Count = count;
            return this;
        }

        public AddProductEntryFactorDto Build()
        {
            return _factor;
        }
    }
}
