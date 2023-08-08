using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Services.Products.Contracts.Dto;

namespace WhiteMarket.TestTools.Products
{
    public class AddProductDtoBuilder
    {
        private readonly AddProductDto _addProductDto;

        public AddProductDtoBuilder(int groupId)
        {
            _addProductDto = new AddProductDto
            {
                Title = "dummy_title",
                GroupId = groupId,
                MinimumInventory = 10
            };
        }

        public AddProductDtoBuilder Title(string title)
        {
            _addProductDto.Title = title;
            return this;
        }
        public AddProductDtoBuilder MinimumInventory(int minInventory)
        {
            _addProductDto.MinimumInventory = minInventory;
            return this;
        }
        

        public AddProductDto Build()
        {
            return _addProductDto;
        }
    }
}
