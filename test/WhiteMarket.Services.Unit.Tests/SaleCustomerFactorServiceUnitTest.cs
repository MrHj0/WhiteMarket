using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.Services.SaleAccountingFactors.Contracts;
using WhiteMarket.Services.SaleCustomerFactors.Contracts;
using WhiteMarket.Services.SaleCustomerFactors.Contracts.Dto;
using WhiteMarket.TestTools.DataBaseConfig;
using WhiteMarket.TestTools.DataBaseConfig.Unit;
using WhiteMarket.TestTools.Groups;
using WhiteMarket.TestTools.Products;
using WhiteMarket.TestTools.SaleCustomerFactors;

namespace WhiteMarket.Services.Unit.Tests
{
    public class SaleCustomerFactorServiceUnitTest : BusinessUnitTest
    {
        [Theory]
        [InlineData(20,5,InventoryStatus.Avalable)]
        [InlineData(20,15,InventoryStatus.LowInventory)]
        [InlineData(20,20,InventoryStatus.UnAvalable)]
        public void Register_register_a_sale_customer_factor_properly(int inventory,
            int boughtCount,InventoryStatus type)
        {
            var dateTime = new Mock<SaleCustomerFactorDateTime>();
            dateTime.Setup(_ => _.Generate()).Returns(DateTime.Now);
            var guid = new Mock<SaleAccountingFactorGuidGenerator>();
            guid.Setup(_ => _.Generate()).Returns(Guid.NewGuid());
            var sut = SaleCustomerFactorAppServiceFactory.Generate(SetupContext,dateTime.Object,guid.Object);
            var group = GroupFactory.Generate();
            var product = new ProductBuilder(group)
                .WithInventory(inventory)
                .Build();
            DbContext.Save(product);
            var dto = new AddSaleCustomerFactorDto
            {
                Count = boughtCount,
                CustomerName = "dummy_customer_name",
                FactorId = "dummy_factor_id",
                Price = 1000,
                ProductId = product.Id,
                ProductName = product.Title
            };

            sut.Register(dto);

            var expectedProduct = ReadContext.Set<Product>().Single();
            expectedProduct.Title.Should().Be(product.Title);
            expectedProduct.Inventory.Should().Be(inventory-boughtCount);
            expectedProduct.Status.Should().Be(type);
            expectedProduct.MinimumInventory.Should().Be(product.MinimumInventory);
            expectedProduct.GroupId.Should().Be(group.Id);

            var expectedCustomerFactor = ReadContext.Set<SaleCustomerFactor>().Single();
            expectedCustomerFactor.ProductName.Should().Be(product.Title);
            expectedCustomerFactor.Count.Should().Be(boughtCount);
            expectedCustomerFactor.Price.Should().Be(dto.Price);
            expectedCustomerFactor.Id.Should().Be(dto.FactorId);
            expectedCustomerFactor.CustomerName.Should().Be(dto.CustomerName);
            expectedCustomerFactor.Date.Should().Be(dateTime.Object.Generate());

            var expectedAccountingFactor = ReadContext.Set<SaleAccountingFactor>().Single();
            expectedAccountingFactor.CustomerFactorId.Should().Be(dto.FactorId);
            expectedAccountingFactor.Id.Should().Be(guid.Object.Generate());
            expectedAccountingFactor.Date.Should().Be(expectedCustomerFactor.Date);
            expectedAccountingFactor.TotalPrice.Should().Be(dto.Price*dto.Count);
        }
    }
}
