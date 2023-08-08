using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Presistence.EF;
using WhiteMarket.Presistence.EF.Products;
using WhiteMarket.Presistence.EF.SaleAccountingFactors;
using WhiteMarket.Presistence.EF.SaleCustomerFactors;
using WhiteMarket.Services.SaleAccountingFactors.Contracts;
using WhiteMarket.Services.SaleCustomerFactors;
using WhiteMarket.Services.SaleCustomerFactors.Contracts;

namespace WhiteMarket.TestTools.SaleCustomerFactors
{
    public class SaleCustomerFactorAppServiceFactory
    {
        public static SaleCustomerFactorAppService Generate(EFDataContext context,
                                                            SaleCustomerFactorDateTime dateTime,
                                                            SaleAccountingFactorGuidGenerator guid)
        {
            return new SaleCustomerFactorAppService(
            new EFProductRepository(context),
            new EFSaleAccountingFactorRepository(context),
            new EFSaleCustomerFactorRepository(context),
            new EFUnitOfWork(context),
            dateTime,
            guid);


        }
    }
}
