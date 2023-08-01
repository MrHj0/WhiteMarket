using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Services.Products.Contracts.Dto;

namespace WhiteMarket.TestTools.Products
{
    public static class AddProductDtoFactory
    {
        public static AddProductDto Generate(int groupId)
        {
            return new AddProductDto
            {
                Title = "dummy_title",
                GroupId = groupId,
                MinimumInventory = 10
            };
        }
    }
}
