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

namespace WhiteMarket.Spec.Tests.Groups.Add
{
    [Scenario("ثبت گروه با نام تکراری")]
    public class FailedDefineGroupWhenGroupNameIsDuplicated : BusinessIntegrationTest
    {
        private Action expected;

        [Given("یک گروه با نام بهداشتی در فهرست گروه وجود دارد")]
        public void Given()
        {
            var group = GroupFactory.Generate("بهداشتی");
            DbContext.Save(group);
        }

        [When("یک گروه با نام بهداشتی را ثبت میکنم")]
        public void When()
        {
            var sut = GroupAppServiceFactory.Generate(SetupContext);
            var dto = AddGroupDtoFactory.Generate("بهداشتی");

            expected = ()=> sut.Define(dto);
        }

        [Then("خطایی با عنوان 'اسم گروه تکراری' باید رخ دهد")]
        public void Then()
        {
            expected.Should().ThrowExactly<GroupNameIsDuplicatedException>();
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
