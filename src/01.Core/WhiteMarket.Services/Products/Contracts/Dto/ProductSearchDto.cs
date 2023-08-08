using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;

namespace WhiteMarket.Services.Products.Contracts.Dto
{
    public class ProductSearchDto
    {
        public string? Title { get; set; }
        public string? GroupName { get; set; }
        public InventoryStatus? Status { get; set; }

    }
}
