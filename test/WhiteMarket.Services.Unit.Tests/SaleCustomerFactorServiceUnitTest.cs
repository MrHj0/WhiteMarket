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
using WhiteMarket.TestTools.DataBaseConfig.Unit;
using WhiteMarket.TestTools.Groups;
using WhiteMarket.TestTools.Products;
using WhiteMarket.TestTools.SaleCustomerFactors;

namespace WhiteMarket.Services.Unit.Tests
{
    public class SaleCustomerFactorServiceUnitTest : BusinessUnitTest
    {
        [Theory]
        [InlineData()]
        public void Register_register_a_sale_customer_factor_properly()
        {
            var dateTime = new Mock<SaleCustomerFactorDateTime>();
            dateTime.Setup(_ => _.Generate()).Returns(DateTime.Now);
            var guid = new Mock<SaleAccountingFactorGuidGenerator>();
            guid.Setup(_ => _.Generate()).Returns(Guid.NewGuid());
            var sut = SaleCustomerFactorAppServiceFactory.Generate(SetupContext,dateTime.Object,guid.Object);
            var group = GroupFactory.Generate();
            var product = ProductFactory.Generate(group);
            var dto = new AddSaleCustomerFactorDto
            {
                Count = 10,
                CustomerName = product.Title,
                FactorId = "dummy_factor_id",
                Price = 1000,
                ProductId = product.Id,
                ProductName = product.Title
            };

            sut.Register(dto);

            var expectedProduct = ReadContext.Set<Product>().Single();
            expectedProduct.Title.Should().Be("لنت ترمز");
            expectedProduct.Inventory.Should().Be(15);
            expectedProduct.Status.Should().Be(InventoryStatus.Avalable);
            expectedProduct.MinimumInventory.Should().Be(5);
            expectedProduct.GroupId.Should().Be(group.Id);

            var expectedCustomerFactor = ReadContext.Set<SaleCustomerFactor>().Single();
            expectedCustomerFactor.ProductName.Should().Be("لنت ترمز");
            expectedCustomerFactor.Count.Should().Be(5);
            expectedCustomerFactor.Price.Should().Be(1000);
            expectedCustomerFactor.Id.Should().Be("۱۲۳آ");
            expectedCustomerFactor.CustomerName.Should().Be("مجید رضوی");
            expectedCustomerFactor.Date.Should().Be(dateTime.Object.Generate());

            var expectedAccountingFactor = ReadContext.Set<SaleAccountingFactor>().Single();
            expectedAccountingFactor.CustomerFactorId.Should().Be("۱۲۳آ");
            expectedAccountingFactor.Id.Should().Be(guid.Object.Generate());
            expectedAccountingFactor.Date.Should().Be(expectedCustomerFactor.Date);
            expectedAccountingFactor.TotalPrice.Should().Be(5000);
        }
    }
}
