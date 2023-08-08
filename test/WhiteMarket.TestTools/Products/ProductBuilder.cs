using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;

namespace WhiteMarket.TestTools.Products
{
    public class ProductBuilder
    {
        private readonly Product _product;

        public ProductBuilder(Group group)
        {
            _product = new Product
            {
                Title = "dummy_title",
                Group = group,
                Inventory = 0,
                MinimumInventory = 10,
                Status = InventoryStatus.UnAvalable
            };
        }

        public ProductBuilder WithTitle(string title)
        {
            _product.Title = title;
            return this;
        }
        public ProductBuilder WithStatus(InventoryStatus status)
        {
            _product.Status = status;
            return this;
        }
        public ProductBuilder WithInventory(int inventory)
        {
            _product.Inventory = inventory;
            return this;
        }
        public ProductBuilder WithMinimumInventry(int minimumInventory)
        {
            _product.MinimumInventory = minimumInventory;
            return this;
        }

        
        public Product Build()
        {
            return _product;
        }
    }
}
