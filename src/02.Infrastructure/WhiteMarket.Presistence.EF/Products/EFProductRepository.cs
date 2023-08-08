using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.Services.Products.Contracts;
using WhiteMarket.Services.Products.Contracts.Dto;

namespace WhiteMarket.Presistence.EF.Products
{
    public class EFProductRepository : ProductRepository
    {
        private readonly DbSet<Product> _products;
        public EFProductRepository(EFDataContext context)
        {
            _products = context.Set<Product>();
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public Product? FindById(int productId)
        {
            return _products.FirstOrDefault(_ => _.Id == productId);
        }

        public HashSet<GetAllProductsDto> GetAll(ProductSearchDto? dto)
        {
            var result = _products.Select(_ => new GetAllProductsDto
            {
                Id = _.Id,
                Title = _.Title,
                GroupName = _.Group.Name,
                Inventory = _.Inventory,
                MInimumInventory = _.MinimumInventory,
                Status = _.Status
            });

            result = ProductSearch(result, dto);

            return result.ToHashSet();
        }

        public bool IsDuplicatedNameByGroupId(int groupId, string title)
        {
            return _products
                .Any(_ => _.GroupId == groupId && _.Title == title);
        }

        public void UpdateProduct(Product product)
        {
            _products.Update(product);
        }

        private IQueryable<GetAllProductsDto>
            ProductSearch(IQueryable<GetAllProductsDto> result, ProductSearchDto? dto)
        {
            if (dto != null && dto.Title != null)
            {
                result = result.Where(_ => _.Title.Contains(dto.Title));
            }

            if (dto != null && dto.GroupName != null)
            {
                result = result.Where(_ => _.GroupName.Contains(dto.GroupName));
            }

            if (dto != null && dto.Status != null)
            {
                result = result.Where(_ => _.Status == dto.Status);
            }
            return result;
        }
    }
}
