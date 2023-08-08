using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;
using WhiteMarket.Services.Groups;
using WhiteMarket.Services.Groups.Contracts;
using WhiteMarket.Services.Groups.Exceptions;
using WhiteMarket.TestTools.DataBaseConfig;
using WhiteMarket.TestTools.DataBaseConfig.Unit;
using WhiteMarket.TestTools.Groups;

namespace WhiteMarket.Services.Unit.Tests
{
    public class GroupServiceUnitTest : BusinessUnitTest
    {
        private readonly GroupService _sut;

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

            var expected = () => _sut.Define(dto);

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

            var expected = () => _sut.Delete(group.Id);

            expected.Should().ThrowExactly<GroupHasProductsException>();
        }

        [Fact]
        public void Delete_throw_exception_when_group_id_is_invalid()
        {
            var invalidId = -1;

            var expected = () => _sut.Delete(invalidId);

            expected.Should().ThrowExactly<GroupNotFoundException>();
        }

        [Fact]
        public void EditGroupName_edit_group_name_properly()
        {
            var group = GroupFactory.Generate();
            DbContext.Save(group);
            var dto = EditGroupNameFactory.Generate();

            _sut.EditGroupName(group.Id, dto);

            var expected = ReadContext.Set<Group>().Single();
            expected.Name.Should().Be(dto.Name);
        }

        [Fact]
        public void EditGroupName_throw_exception_when_group_id_is_invalid()
        {
            var invalidId = -1;
            var dto = EditGroupNameFactory.Generate();

            var expected = () => _sut.EditGroupName(invalidId, dto);

            expected.Should().ThrowExactly<GroupNotFoundException>();
        }

        [Fact]
        public void EditGroupName_throw_exception_when_group_name_is_duplicated()
        {
            var group1 = GroupFactory.Generate();
            DbContext.Save(group1);
            var group2 = GroupFactory.Generate("dummy_name_2");
            DbContext.Save(group2);
            var dto = EditGroupNameFactory.Generate();

            var expected = () => _sut.EditGroupName(group2.Id, dto);

            expected.Should().ThrowExactly<GroupNameIsDuplicatedException>();
        }

        [Fact]
        public void GetAllGroups_get_all_groups_properly()
        {
            var group = GroupFactory.Generate();
            DbContext.Save(group);

            var expected = _sut.GetAllGroups();

            expected.Should().HaveCount(1);
            expected.Single().Name.Should().Be(group.Name);
        }

        [Fact]
        public void GetOneGroupWithProducts_get_one_group_with_products()
        {
            var group = GroupFactory.Generate();
            var product = new Product
            {
                Title = "dummy-title",
                Group = group
            };
            DbContext.Save(product);

            var expected = _sut.GetOneGroupWithProducts(group.Id);
            expected.Id.Should().Be(group.Id);
            expected.Name.Should().Be(group.Name);
            expected.Products.Should().HaveCount(1);
            expected.Products.Single().Should().Be(product.Title);
        }
    }
}
