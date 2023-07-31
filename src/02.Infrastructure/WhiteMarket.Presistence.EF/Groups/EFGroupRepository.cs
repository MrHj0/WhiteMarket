using Microsoft.EntityFrameworkCore;
using WhiteMarket.Entities;
using WhiteMarket.Presistence.EF;
using WhiteMarket.Services.Groups.Contracts;

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

        public Group? GetGroupById(int groupId)
        {
            return _groups
                .Where(_ => _.Id == groupId)
                .Include(_=>_.Products)
                .FirstOrDefault();
        }

        public bool IsDuplicatedName(string name)
        {
            return _groups.Any(_ => _.Name == name);
        }
    }
}
