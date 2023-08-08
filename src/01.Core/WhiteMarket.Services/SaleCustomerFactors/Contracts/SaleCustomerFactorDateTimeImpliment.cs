using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteMarket.Services.SaleCustomerFactors.Contracts
{
    public class SaleCustomerFactorDateTimeImpliment : SaleCustomerFactorDateTime
    {
        public DateTime Generate()
        {
            return DateTime.Now;
        }
    }
}
