using WhiteMarket.Entities;

namespace WhiteMarket.Services.Groups.Contracts
{
    public interface GroupRepository
    {
        void Add(Group group);
        bool IsDuplicatedName(string name);
        void Delete(Group group);
        Group? GetGroupById(int groupId);
    }
}
