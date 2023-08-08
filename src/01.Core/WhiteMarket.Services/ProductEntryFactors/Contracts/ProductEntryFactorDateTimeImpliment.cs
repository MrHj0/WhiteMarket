using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteMarket.Services.ProductEntryFactors.Contracts
{
    public class ProductEntryFactorDateTimeImpliment : ProductEntryFactorDateTime
    {
        public DateTime Generate()
        {
            return DateTime.Now;
        }
    }
}
