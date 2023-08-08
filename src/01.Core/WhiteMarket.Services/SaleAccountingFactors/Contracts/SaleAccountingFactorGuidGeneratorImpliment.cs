using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteMarket.Services.SaleAccountingFactors.Contracts
{
    public class SaleAccountingFactorGuidGeneratorImpliment : SaleAccountingFactorGuidGenerator
    {
        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}
