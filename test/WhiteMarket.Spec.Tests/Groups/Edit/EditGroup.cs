using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.TestTools.DataBaseConfig;
using WhiteMarket.TestTools.DataBaseConfig.Integration;
using WhiteMarket.TestTools.Groups;

namespace WhiteMarket.Spec.Tests.Groups.Edit
{
    [Scenario("ویرایش دسته بندی ")]
    public class EditGroup : BusinessIntegrationTest
    {
        private Group _group;

        [Given("دسته بندی با نام دیجیتال در فهرست دسته بندی ها موجود است")]
        public void Given()
        {
            _group = GroupFactory.Generate("دیجیتال");
            DbContext.Save(_group);
        }

        [When("نام دسته بندی دیجیتال را به لبنیات تغییر میدهم")]
        public void When()
        {
            var sut = GroupAppServiceFactory.Generate(SetupContext);

            sut.EditGroupName(_group.Id, "لبنیات");
        }

        [Then("نام دسته بندی دیجیتال به لبنیات باید تغییر کرده باشد")]
        public void Then()
        {
            var expected = ReadContext.Set<Group>().Single();
            expected.Name.Should().Be("لبنیات");
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
