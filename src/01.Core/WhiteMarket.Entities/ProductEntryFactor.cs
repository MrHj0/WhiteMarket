using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteMarket.Entities
{
    public class ProductEntryFactor
    {
        public int DummyPrimaryKey { get; set; }
        public string Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductEntryCount { get; set; }
        public string CompanyName { get; set; }
        public DateTime Date { get; set; }

    }
}
