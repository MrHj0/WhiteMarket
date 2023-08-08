using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteMarket.Services.SaleCustomerFactors.Contracts.Dto
{
    public class AddSaleCustomerFactorDto
    {
        [Required,MaxLength(50)]
        public string FactorId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public int Price { get; set; }
        [Required,MaxLength(50)]
        public string CustomerName { get; set; }
        public int Count { get; set; }
    }
}
