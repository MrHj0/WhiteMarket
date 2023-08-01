﻿using WhiteMarket.Services.Groups.Contracts.Dto;

namespace WhiteMarket.Services.Groups.Contracts
{
    public interface GroupService
    {
        void Define(AddGroupDto dto);
        void Delete(int groupId);
        void EditGroupName(int groupId, string name);
        HashSet<GetAllGroupsDto> GetAllGroups();
    }
}
