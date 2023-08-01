using WhiteMarket.Entities;

namespace WhiteMarket.Services.Groups.Contracts
{
    public interface GroupRepository
    {
        void Add(Group group);
        void Delete(Group group);
        bool IsDuplicatedName(string name);
        bool IsDuplicatedNameExceptThisGroup(int groupId,string name);
        Group? GetGroupById(int groupId);
    }
}
