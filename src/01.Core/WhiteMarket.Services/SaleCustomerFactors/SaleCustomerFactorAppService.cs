using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.Services.Contracts;
using WhiteMarket.Services.Products.Contracts;
using WhiteMarket.Services.SaleAccountingFactors.Contracts;
using WhiteMarket.Services.SaleCustomerFactors.Contracts;
using WhiteMarket.Services.SaleCustomerFactors.Contracts.Dto;

namespace WhiteMarket.Services.SaleCustomerFactors
{
    public class SaleCustomerFactorAppService : SaleCustomerFactorService
    {
        private readonly ProductRepository _productRepository;
        private readonly SaleCustomerFactorRepository _customerFactorRepository;
        private readonly SaleAccountingFactorRepository _accountingFactorRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly SaleCustomerFactorDateTime _dateTime;
        private readonly SaleAccountingFactorGuidGenerator _accountingFactorGuidGenerator;

        public SaleCustomerFactorAppService(ProductRepository productRepository,
                                            SaleAccountingFactorRepository saleAccountingFactorRepository,
                                            SaleCustomerFactorRepository saleCustomerFactorRepository,
                                            UnitOfWork unitOfWork,
                                            SaleCustomerFactorDateTime dateTime,
                                            SaleAccountingFactorGuidGenerator accountingFactorGuidGenerator)
        {
            _productRepository = productRepository;
            _customerFactorRepository = saleCustomerFactorRepository;
            _accountingFactorRepository = saleAccountingFactorRepository;
            _unitOfWork = unitOfWork;
            _dateTime = dateTime;
            _accountingFactorGuidGenerator = accountingFactorGuidGenerator;
        }

        public void Register(AddSaleCustomerFactorDto dto)
        {
            var product = _productRepository.FindById(dto.ProductId);

            product.Inventory = product.Inventory - dto.Count;
            if (product.Inventory > product.MinimumInventory)
            {
                product.Status = InventoryStatus.Avalable;
            }

            else if(product.Inventory < product.MinimumInventory && product.Inventory != 0)
            {
                product.Status = InventoryStatus.LowInventory;
            }

            else if(product.Inventory == 0)
            {
                product.Status = InventoryStatus.UnAvalable;
            }
            var customerFactor = new SaleCustomerFactor
            {
                Count = dto.Count,
                CustomerName = dto.CustomerName,
                Date = _dateTime.Generate(),
                Id = dto.FactorId,
                Price = dto.Price,
                ProductId = dto.ProductId,
                ProductName = product.Title
            };
            var accountingFactor = new SaleAccountingFactor
            {
                Id = _accountingFactorGuidGenerator.Generate(),
                CustomerFactorId = customerFactor.Id,
                Date = customerFactor.Date,
                TotalPrice = customerFactor.Price * customerFactor.Count
            };

            _productRepository.UpdateProduct(product);
            _accountingFactorRepository.Add(accountingFactor);
            _customerFactorRepository.Add(customerFactor);
            _unitOfWork.Complete();
        }


    }
}
