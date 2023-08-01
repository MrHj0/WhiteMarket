using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.Services.Products.Contracts;

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

        public bool IsDuplicatedNameByGroupId(int groupId,string title)
        {
            return _products
                .Any(_=>_.GroupId == groupId && _.Title == title);
        }
    }
}
