using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteMarket.Services.Groups.Contracts.Dto
{
    public class GetOneGroupWithProductsDto
    {
        public GetOneGroupWithProductsDto()
        {
            Products = new();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public HashSet<string> Products { get; set; }
    }

}
