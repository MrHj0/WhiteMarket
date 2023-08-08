using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.Services.Products.Contracts.Dto;

namespace WhiteMarket.Services.Products.Contracts
{
    public interface ProductRepository
    {
        void Add(Product product);
        void UpdateProduct(Product product);
        bool IsDuplicatedNameByGroupId(int groupId,string title);
        HashSet<GetAllProductsDto> GetAll(ProductSearchDto? dto);
        Product? FindById(int productId);
    }
}
