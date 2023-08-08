using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;

namespace WhiteMarket.Services.Products.Contracts.Dto
{
    public class GetAllProductsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string GroupName { get; set; }
        public int Inventory { get; set; }
        public int MInimumInventory { get; set; }
        public InventoryStatus Status { get; set; }

    }
}
