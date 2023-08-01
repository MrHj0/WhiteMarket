using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;

namespace WhiteMarket.Services.Products.Contracts.Dto
{
    public class AddProductDto
    {
        [Required,MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public int GroupId { get; set; }
        [Required]
        public int MinimumInventory { get; set; }
    }
}
