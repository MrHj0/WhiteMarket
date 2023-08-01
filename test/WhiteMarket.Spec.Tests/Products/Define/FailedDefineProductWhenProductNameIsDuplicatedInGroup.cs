using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.Services.Products.Contracts.Dto;
using WhiteMarket.Services.Products.Exceptions;
using WhiteMarket.TestTools.DataBaseConfig;
using WhiteMarket.TestTools.DataBaseConfig.Integration;
using WhiteMarket.TestTools.Groups;
using WhiteMarket.TestTools.Products;

namespace WhiteMarket.Spec.Tests.Products.Define
{
    [Scenario("تعریف کالا با عنوان تکراری در یک گروه")]
    public class FailedDefineProductWhenProductNameIsDuplicatedInGroup : BusinessIntegrationTest
    {
        private Group _group;
        private Action expected;

        [Given("یک گروه با عنوان بهداشتی در فهرست گروه ها وجود دارد" +
                "و: یک کالا با نام شامپو در گروه بهداشتی وجود دارد")]
        public void Given()
        {
            _group = GroupFactory.Generate("بهداشتی");
            var product = ProductFactory.Generate(_group, "شامپو");
            DbContext.Save(product);
        }

        [When("یک کالا با عنوان شامپو در گروه بهداشتی و حداقل موجودی ۱۰ را ثبت میکنم ")]

        public void When()
        {
            var sut = ProductAppServiceFactory.Generate(SetupContext);

            var dto = new AddProductDto
            {
                Title = "شامپو",
                GroupId = _group.Id,
                MinimumInventory = 10
            };

            expected = () => sut.Define(dto);
        }

        [Then("خطایی با عنوان 'عنوان کالا تکراری است' باید رخ دهد")]
        public void Then()
        {
            expected.Should().ThrowExactly<ProductNameIsDuplicatedInThisGroupExcepton>();
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
