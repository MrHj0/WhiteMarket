using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.Services.Groups.Exceptions;
using WhiteMarket.TestTools.DataBaseConfig;
using WhiteMarket.TestTools.DataBaseConfig.Integration;
using WhiteMarket.TestTools.Groups;

namespace WhiteMarket.Spec.Tests.Groups.Delete
{
    [Scenario("حذف گروه وقتی که برای گروه کالا وجود دارد")]
    public class FailedDeleteGroupWhenGroupHasProducts : BusinessIntegrationTest
    {
        private Action expected;
        private Group _group;

        [Given("در فهرست گروه ها یک گروه به نام بهداشتی وجود دارد" +
               "و: " +
               "یک کالا با عنوان شامپو در گروه بهداشتی ثبت شده است")]
        public void Given()
        {
            _group = GroupFactory.Generate("بهداشتی");
            var product = new Product
            {
                Title = "شامپو",
                Inventory = 0,
                MinimumInventory = 10,
                Status = InventoryStatus.UnAvalable,
                Group = _group
            };
            DbContext.Save(product);
        }

        [When("گروه بهداشتی را حذف میکنم")]
        public void When()
        {
            var sut = GroupAppServiceFactory.Generate(SetupContext);

            expected = ()=> sut.Delete(_group.Id);
        }

        [Then("خطایی با عنوان 'گروه دارای کالا است' باید رخ دهد")]
        public void Then()
        {
            expected.Should().ThrowExactly<GroupHasProductsException>();
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
