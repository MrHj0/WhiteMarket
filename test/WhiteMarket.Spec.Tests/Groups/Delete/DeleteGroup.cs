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

namespace WhiteMarket.Spec.Tests.Groups.Delete
{
    [Scenario("حذف گروه")]
    public class DeleteGroup : BusinessIntegrationTest
    {
        private Group _group;

        [Given("در فهرست گروه ها یک گروه به نام بهداشتی وجود دارد")]
        public void Given()
        {
            _group = GroupFactory.Generate("بهداشتی");
            DbContext.Save(_group);
        }

        [When("گروه بهداشتی را حذف میکنم")]
        public void When()
        {
            var sut = GroupAppServiceFactory.Generate(SetupContext);

            sut.Delete(_group.Id);
        }

        [Then("در فهرست گروه ها نباید گروهی وجود داشته باشد")]
        public void Then()
        {
            var expected = ReadContext.Set<Group>();
            expected.Should().HaveCount(0);
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
