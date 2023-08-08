using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteMarket.Services.ProductEntryFactors.Contracts.Dto
{
    public class AddProductEntryFactorDto
    {
        [Required,MaxLength(50)]
        public string FactorId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required,Range(1,1000)]
        public int Count { get; set; }
        [Required,MaxLength(50)]
        public string CompanyName { get; set; }
    }
}
