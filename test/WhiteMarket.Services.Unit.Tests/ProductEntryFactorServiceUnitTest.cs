using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.Presistence.EF;
using WhiteMarket.Presistence.EF.ProductEntryFactors;
using WhiteMarket.Presistence.EF.Products;
using WhiteMarket.Services.ProductEntryFactors;
using WhiteMarket.Services.ProductEntryFactors.Contracts;
using WhiteMarket.Services.ProductEntryFactors.Contracts.Dto;
using WhiteMarket.Services.Products.Exceptions;
using WhiteMarket.TestTools.DataBaseConfig;
using WhiteMarket.TestTools.DataBaseConfig.Unit;
using WhiteMarket.TestTools.Groups;
using WhiteMarket.TestTools.ProductEntryFactors;
using WhiteMarket.TestTools.Products;

namespace WhiteMarket.Services.Unit.Tests
{
    public class ProductEntryFactorServiceUnitTest : BusinessUnitTest
    {
        private readonly ProductEntryFactorService _sut;
        private DateTime _date;
        public ProductEntryFactorServiceUnitTest()
        {
            var dateTime = new Mock<ProductEntryFactorDateTime>();
            dateTime.Setup(_ => _.Generate()).Returns(DateTime.Now);
            _date = dateTime.Object.Generate();
            _sut = ProductEntryFactorAppServiceFactory.Generate(SetupContext,dateTime.Object);
        }
        [Theory]
        [InlineData(10,20)]
        [InlineData(10,10)]
        [InlineData(10,5)]
        public void Register_register_a_product_entry_factor_properly(int minimumInventory,int count)
        {
            var group = GroupFactory.Generate();
            var product = new ProductBuilder(group)
                .WithMinimumInventry(minimumInventory)
                .Build();
            DbContext.Save(product);
            var dto = new AddProductEntryFactorDtoBuilder(product.Id)
                .WithCount(count)
                .Build();

            _sut.Register(dto);

            var expected = ReadContext.Set<ProductEntryFactor>().Single();
            expected.Id.Should().Be(dto.FactorId);
            expected.CompanyName.Should().Be(dto.CompanyName);
            expected.ProductEntryCount.Should().Be(dto.Count);
            expected.ProductId.Should().Be(product.Id);
            expected.ProductName.Should().Be(product.Title);
            expected.Date.Should().Be(_date);
        }

        [Fact]
        public void Register_throw_exception_when_product_id_is_invalid()
        {
            var invalidId = -1;
            var dto = AddProductEntryFactorDtoFactory.Generate(invalidId);

            var expected = ()=> _sut.Register(dto);

            expected.Should().ThrowExactly<ProductNotFoundException>();
        }
    }
}
