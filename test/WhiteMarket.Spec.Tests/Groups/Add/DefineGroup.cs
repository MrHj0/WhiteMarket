using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.Presistence.EF;
using WhiteMarket.Presistence.EF.Groups;
using WhiteMarket.Services.Groups;
using WhiteMarket.Services.Groups.Contracts.Dto;
using WhiteMarket.TestTools.DataBaseConfig.Integration;
using WhiteMarket.TestTools.Groups;

namespace WhiteMarket.Spec.Tests.Groups.Add
{
    [Scenario("ثبت گروه")]
    public class DefineGroup : BusinessIntegrationTest
    {
        [Given("فهرست گروه ها خالی است")]
        public void Given()
        {

        }

        [When("یک گروه با نام بهداشتی را ثبت میکنم ")]
        public void When()
        {
            var sut = GroupAppServiceFactory.Generate(SetupContext);
            var dto = AddGroupDtoFactory.Generate("بهداشتی");

            sut.Define(dto);
        }

        [Then("در فهرست گروه ها یک گروه با نام بهداشتی باید وجود داشته باشد")]
        public void Then()
        {
            var expected = ReadContext.Set<Group>().Single();
            expected.Name.Should().Be("بهداشتی");
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
