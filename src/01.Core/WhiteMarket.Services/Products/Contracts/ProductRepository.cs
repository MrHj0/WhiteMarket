using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;

namespace WhiteMarket.Services.Products.Contracts
{
    public interface ProductRepository
    {
        void Add(Product product);
        bool IsDuplicatedNameByGroupId(int groupId,string title);
    }
}
