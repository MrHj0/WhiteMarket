using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.Services.Contracts;
using WhiteMarket.Services.ProductEntryFactors.Contracts;
using WhiteMarket.Services.ProductEntryFactors.Contracts.Dto;
using WhiteMarket.Services.Products.Contracts;
using WhiteMarket.Services.Products.Exceptions;

namespace WhiteMarket.Services.ProductEntryFactors
{
    public class ProductEntryFactorAppService : ProductEntryFactorService
    {
        private readonly ProductRepository _productRepository;
        private readonly ProductEntryFactorRepository _productEntryFactorRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly ProductEntryFactorDateTime _dateTime;

        public ProductEntryFactorAppService(ProductRepository productRepository,
                                          ProductEntryFactorRepository productEntryFactorRepository,
                                          UnitOfWork unitOfWork,
                                          ProductEntryFactorDateTime dateTime)
        {
            _productRepository = productRepository;
            _productEntryFactorRepository = productEntryFactorRepository;
            _unitOfWork = unitOfWork;
            _dateTime = dateTime;
        }


        public void Register(AddProductEntryFactorDto dto)
        {
            var product = _productRepository.FindById(dto.ProductId);

            StopIfProductWasNotFound(product);

            product.Inventory = dto.Count;

            SetupTheProductStatus(product,dto.Count);
            var factor = new ProductEntryFactor
            {
                CompanyName = dto.CompanyName,
                ProductEntryCount = dto.Count,
                ProductId = dto.ProductId,
                Id = dto.FactorId,
                ProductName = product.Title,
                Date = _dateTime.Generate()
            };

            _productEntryFactorRepository.Add(factor);
            _productRepository.UpdateProduct(product);
            _unitOfWork.Complete();
        }


        private void StopIfProductWasNotFound(Product? product)
        {
            if (product == null)
            {
                throw new ProductNotFoundException();
            }
        }
        private void SetupTheProductStatus(Product product, int inventory)
        {
            if(product.Inventory > product.MinimumInventory)
            {
                product.Status = InventoryStatus.Avalable;
            }
            else
            {
                product.Status = InventoryStatus.LowInventory;
            }
        }
    }
}
