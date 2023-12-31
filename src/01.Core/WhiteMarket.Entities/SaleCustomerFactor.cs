﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteMarket.Entities
{
    public class SaleCustomerFactor
    {
        public int PrimaryKey { get; set; }
        public string Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
    }
}
