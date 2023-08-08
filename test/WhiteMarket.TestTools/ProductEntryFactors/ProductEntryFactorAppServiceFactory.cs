using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Presistence.EF;
using WhiteMarket.Presistence.EF.ProductEntryFactors;
using WhiteMarket.Presistence.EF.Products;
using WhiteMarket.Services.ProductEntryFactors;
using WhiteMarket.Services.ProductEntryFactors.Contracts;

namespace WhiteMarket.TestTools.ProductEntryFactors
{
    public static class ProductEntryFactorAppServiceFactory
    {
        public static ProductEntryFactorAppService Generate(EFDataContext context,
                                                            ProductEntryFactorDateTime dateTime)
        {
            
            return new ProductEntryFactorAppService(new EFProductRepository(context),
                                                    new EFProductEntryFactorRepository(context),
                                                    new EFUnitOfWork(context),
                                                    dateTime );
        }
    }
}
