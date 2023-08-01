using Microsoft.EntityFrameworkCore;
using WhiteMarket.Entities;
using WhiteMarket.Presistence.EF;
using WhiteMarket.Services.Groups.Contracts;
using WhiteMarket.Services.Groups.Contracts.Dto;

namespace WhiteMarket.Presistence.EF.Groups
{
    public class EFGroupRepository : GroupRepository
    {
        private readonly DbSet<Group> _groups;

        public EFGroupRepository(EFDataContext groups)
        {
            _groups = groups.Set<Group>();
        }

        public void Add(Group group)
        {
            _groups.Add(group);
        }

        public void Delete(Group group)
        {
            _groups.Remove(group);
        }

        public HashSet<GetAllGroupsDto> GetAllGroups()
        {
            return _groups.Select(_ => new GetAllGroupsDto
            {

                Id = _.Id,
                Name = _.Name
            }).ToHashSet();
        }

        public Group? GetGroupById(int groupId)
        {
            return _groups
                .Where(_ => _.Id == groupId)
                .Include(_ => _.Products)
                .FirstOrDefault();
        }

        public GetOneGroupWithProductsDto GetOneGroupWithProductsByGroupId(int groupId)
        {
            return _groups
                .Where(_ => _.Id == groupId)
                .Select(_ => new GetOneGroupWithProductsDto
                {
                    Id = _.Id,
                    Name = _.Name,
                    Products = _.Products.Select(p => p.Title).ToHashSet()
                }).First();
        }

        public bool IsDuplicatedName(string name)
        {
            return _groups.Any(_ => _.Name == name);
        }

        public bool IsDuplicatedNameExceptThisGroup(int groupId, string name)
        {
            return _groups
                .Any(_ => _.Name == name && _.Id != groupId);
        }

        public bool IsGroupExsistByGroupId(int groupId)
        {
            return _groups.Any(_ => _.Id == groupId);
        }
    }
}
