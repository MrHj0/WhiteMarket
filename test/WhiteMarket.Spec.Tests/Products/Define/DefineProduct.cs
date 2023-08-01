using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.Presistence.EF;
using WhiteMarket.Presistence.EF.Products;
using WhiteMarket.Services.Products;
using WhiteMarket.Services.Products.Contracts.Dto;
using WhiteMarket.TestTools.DataBaseConfig;
using WhiteMarket.TestTools.DataBaseConfig.Integration;
using WhiteMarket.TestTools.Groups;
using WhiteMarket.TestTools.Products;

namespace WhiteMarket.Spec.Tests.Products.Define
{
    [Scenario("تعریف کالا ")]
    public class DefineProduct : BusinessIntegrationTest
    {
        private Group _group;

        [Given("دو گروه با عنوان های  اسباب بازی و لبنیات " +
            "در فهرست گروه ها وجود دارد"+
            "و: یک کالا با عنوان شیر در فهرست کالا های لبنیات وجود دارد")]
        public void Given()
        {
            _group = GroupFactory.Generate("اسباب بازی");
            var group = GroupFactory.Generate("لبنیات");
            DbContext.SaveRange(_group,group);
        }

        [When("یک کالا با عنوان شیر " +
            "در گروه اسباب بازی با حداقل موجودی ۱۰ را ثبت میکنم ")]

        public void When()
        {
            var sut = ProductAppServiceFactory.Generate(SetupContext);

            var dto = new AddProductDto
            {
                Title = "شیر",
                GroupId = _group.Id,
                MinimumInventory = 10
            };

            sut.Define(dto);
        }

        [Then("یک کالا با عنوان شیر در گروه اسباب بازی " +
            "و حداقل موجودی ۱۰ و وضعیت ناموجود " +
            "و موجودی ۰  باید فهرست کالا موجود باشد")]
        public void Then()
        {
            var expected = ReadContext.Set<Product>().Single();
            expected.Title.Should().Be("شیر");
            expected.GroupId.Should().Be(_group.Id);
            expected.MinimumInventory.Should().Be(10);
            expected.Status.Should().Be(InventoryStatus.UnAvalable);
            expected.Inventory.Should().Be(0);
        }

        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given(),
                _ => When(),
                _ => Then());
        }
    }
}
