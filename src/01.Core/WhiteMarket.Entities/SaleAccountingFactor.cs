using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteMarket.Entities
{
    public class SaleAccountingFactor
    {
        public Guid Id { get; set; }
        public string CustomerFactorId { get; set; }
        public int TotalPrice { get; set; }
        public DateTime Date { get; set; }
    }
}
