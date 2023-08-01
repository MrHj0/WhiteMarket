using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.Presistence.EF;
using WhiteMarket.Presistence.EF.Products;
using WhiteMarket.Services.Groups.Exceptions;
using WhiteMarket.Services.Products;
using WhiteMarket.Services.Products.Contracts.Dto;
using WhiteMarket.Services.Products.Exceptions;
using WhiteMarket.TestTools.DataBaseConfig;
using WhiteMarket.TestTools.DataBaseConfig.Unit;
using WhiteMarket.TestTools.Groups;
using WhiteMarket.TestTools.Products;

namespace WhiteMarket.Services.Unit.Tests
{
    public class ProductServiceUnitTest : BusinessUnitTest
    {
        private readonly ProductAppService _sut;
        public ProductServiceUnitTest()
        {
            _sut = ProductAppServiceFactory.Generate(SetupContext);
        }

        [Fact]
        public void Define_define_product_properly()
        {
            var group = GroupFactory.Generate();
            DbContext.Save(group);
            var dto = AddProductDtoFactory.Generate(group.Id);

            _sut.Define(dto);

            var expected = ReadContext.Set<Product>().Single();
            expected.Title.Should().Be(dto.Title);
            expected.Inventory.Should().Be(0);
            expected.GroupId.Should().Be(group.Id);
            expected.MinimumInventory.Should().Be(dto.MinimumInventory);
            expected.Status.Should().Be(InventoryStatus.UnAvalable);
        }

        [Fact]
        public void Define_throw_exception_when_group_id_is_invalid()
        {
            var invalidId = -1;
            var dto = AddProductDtoFactory.Generate(invalidId);

            var expected = () => _sut.Define(dto);

            expected.Should().ThrowExactly<GroupNotFoundException>();
        }

        [Fact]
        public void Define()
        {
            var group = GroupFactory.Generate();
            var product = ProductFactory.Generate(group);
            DbContext.Save(product);
            var dto = AddProductDtoFactory.Generate(group.Id);

            var excepted = ()=> _sut.Define(dto);

            excepted.Should().ThrowExactly<ProductNameIsDuplicatedInThisGroupExcepton>();
        }
    }
}
