using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.Services.Contracts;
using WhiteMarket.Services.Groups.Contracts;
using WhiteMarket.Services.Groups.Exceptions;
using WhiteMarket.Services.Products.Contracts;
using WhiteMarket.Services.Products.Contracts.Dto;
using WhiteMarket.Services.Products.Exceptions;

namespace WhiteMarket.Services.Products
{
    public class ProductAppService : ProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly GroupRepository _groupRepository;
        public ProductAppService(ProductRepository productRepository,
                                 UnitOfWork unitOfWork,
                                 GroupRepository groupRepository)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
        }

        public void Define(AddProductDto dto)
        {
            StopIfGroupIdWasInvalid(dto.GroupId);
            StopIfProductNameWasDuplicated(dto.GroupId, dto.Title);

            var product = CreateProduct(dto);

            _productRepository.Add(product);
            _unitOfWork.Complete();
        }


        private Product CreateProduct(AddProductDto dto)
        {
            return new Product
            {
                Title = dto.Title,
                GroupId = dto.GroupId,
                MinimumInventory = dto.MinimumInventory,
                Inventory = 0,
                Status = InventoryStatus.UnAvalable
            };
        }
        private void StopIfProductNameWasDuplicated(int groupId, string title)
        {
            var isDuplicateName = _productRepository
                .IsDuplicatedNameByGroupId(groupId, title);
            if (isDuplicateName)
            {
                throw new ProductNameIsDuplicatedInThisGroupExcepton();
            }
        }
        private void StopIfGroupIdWasInvalid(int groupId)
        {
            var isexsistGroup = _groupRepository
                .IsGroupExsistByGroupId(groupId);
            if (!isexsistGroup)
            {
                throw new GroupNotFoundException();
            }
        }
    }
}
