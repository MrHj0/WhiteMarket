using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Presistence.EF;
using WhiteMarket.Presistence.EF.Groups;
using WhiteMarket.Services.Groups;

namespace WhiteMarket.TestTools.Groups
{
    public static class GroupAppServiceFactory
    {
        public static GroupAppService Generate(EFDataContext context)
        {
            return new GroupAppService(new EFGroupRepository(context)
                                      , new EFUnitOfWork(context));
        }
    }
}
