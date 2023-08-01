using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Presistence.EF;
using WhiteMarket.Presistence.EF.Groups;
using WhiteMarket.Presistence.EF.Products;
using WhiteMarket.Services.Products;

namespace WhiteMarket.TestTools.Products
{
    public static class ProductAppServiceFactory
    {
        public static ProductAppService Generate(EFDataContext context)
        {
            return new ProductAppService(new EFProductRepository(context),
                                         new EFUnitOfWork(context),
                                         new EFGroupRepository(context));
        }
    }
}
