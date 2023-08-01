using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.Services.Groups;
using WhiteMarket.Services.Groups.Exceptions;
using WhiteMarket.TestTools.DataBaseConfig;
using WhiteMarket.TestTools.DataBaseConfig.Unit;
using WhiteMarket.TestTools.Groups;

namespace WhiteMarket.Services.Unit.Tests
{
    public class GroupServiceUnitTest : BusinessUnitTest
    {
        private readonly GroupAppService _sut;

        public GroupServiceUnitTest()
        {
            _sut = GroupAppServiceFactory.Generate(SetupContext);
        }
        [Fact]
        public void Define_define_a_group_properly()
        {
            var dto = AddGroupDtoFactory.Generate();

            _sut.Define(dto);

            var expected = ReadContext.Set<Group>().Single();
            expected.Name.Should().Be(dto.Name);
        }

        [Fact]
        public void Define_throw_exception_when_group_name_is_duplicated()
        {
            var group = GroupFactory.Generate();
            DbContext.Save(group);
            var dto = AddGroupDtoFactory.Generate();

            var expected = ()=> _sut.Define(dto);

            expected.Should().ThrowExactly<GroupNameIsDuplicatedException>();
        }

        [Fact]
        public void Delete_delete_a_group_properly()
        {
            var group = GroupFactory.Generate();
            DbContext.Save(group);

            _sut.Delete(group.Id);

            var expected = ReadContext.Set<Group>();
            expected.Should().HaveCount(0);
        }

        [Fact]
        public void Delete_throw_exception_when_group_has_product()
        {
            var group = GroupFactory.Generate();
            var product = new Product
            {
                Title = "dummy",
                Group = group,
            };
            DbContext.Save(product);

            var expected = ()=> _sut.Delete(group.Id);

            expected.Should().ThrowExactly<GroupHasProductsException>();
        }

        [Fact]
        public void Delete_throw_exception_when_group_id_is_invalid()
        {
            var invalidId = -1;

            var expected = ()=> _sut.Delete(invalidId);

            expected.Should().ThrowExactly<GroupNotFoundException>();
        }
    }
}
