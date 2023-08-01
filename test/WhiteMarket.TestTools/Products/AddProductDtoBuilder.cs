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
        private static AddProductDto _addProductDto;

        public AddProductDtoBuilder(int groupId)
        {
            _addProductDto.GroupId = groupId;
        }

        public static void Title(string title)
        {
            _addProductDto.Title = title;
        }
        public static void MinimumInventory(int minInventory)
        {
            _addProductDto.MinimumInventory = minInventory;
        }


        public static AddProductDto Build()
        {
            return _addProductDto;
        }
    }
}
