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

namespace WhiteMarket.Spec.Tests.Groups.Edit
{
    [Scenario("ویرایش دسته بندی با نام تکراری ")]
    public class FailedEditGroupNameWhenNameIsDuplicated : BusinessIntegrationTest
    {
        private Group _group;
        private Group _group2;
        private Action _expected;

        [Given("دو دسته بندی با نام های پلاسکو و دیجیتال " +
            "در فهرست دسته بندی ها وجود دارند")]
        public void Given()
        {
            _group = GroupFactory.Generate("دیجیتال");
            DbContext.Save(_group);
            _group2 = GroupFactory.Generate("پلاسکو");
            DbContext.Save(_group2);
        }

        [When("نام دسته بندی پلاسکو را به دیجیتال تغییر میدهم ")]
        public void When()
        {
            var sut = GroupAppServiceFactory.Generate(SetupContext);

            _expected = () => sut.EditGroupName(_group2.Id, "دیجیتال");
        }

        [Then("خطایی با عنوان 'اسم گروه تکراری' باید رخ دهد")]
        public void Then()
        {
            _expected.Should().ThrowExactly<GroupNameIsDuplicatedException>();
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
