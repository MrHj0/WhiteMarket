using WhiteMarket.Entities;
using WhiteMarket.Services.Groups.Contracts.Dto;

namespace WhiteMarket.Services.Groups.Contracts
{
    public interface GroupRepository
    {
        void Add(Group group);
        void Delete(Group group);
        bool IsDuplicatedName(string name);
        bool IsDuplicatedNameExceptThisGroup(int groupId,string name);
        bool IsGroupExsistByGroupId(int groupId);
        Group? GetGroupById(int groupId);
        HashSet<GetAllGroupsDto> GetAllGroups();
        GetOneGroupWithProductsDto GetOneGroupWithProductsByGroupId(int groupId);
    }
}
