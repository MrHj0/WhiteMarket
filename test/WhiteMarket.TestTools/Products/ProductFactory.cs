using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;

namespace WhiteMarket.TestTools.Products
{
    public static class ProductFactory
    {
        public static Product Generate(Group group,string title = "dummy_title")
        {
            return new Product
            {
                Title = title,
                Group = group,
                Inventory = 0,
                MinimumInventory = 10,
                Status = InventoryStatus.UnAvalable
            };
        }
    }
}
