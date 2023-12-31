﻿using WhiteMarket.Entities;
using WhiteMarket.Services.Contracts;
using WhiteMarket.Services.Groups.Contracts;
using WhiteMarket.Services.Groups.Contracts.Dto;
using WhiteMarket.Services.Groups.Exceptions;

namespace WhiteMarket.Services.Groups
{
    public class GroupAppService : GroupService
    {
        private readonly GroupRepository _groupRepository;
        private readonly UnitOfWork _unitOfWork;

        public GroupAppService(GroupRepository groupRepository,
                               UnitOfWork unitOfWork)
        {
            _groupRepository = groupRepository;
            _unitOfWork = unitOfWork;
        }

        public void Define(AddGroupDto dto)
        {
            StopIfGroupNameIsDuplicated(dto.Name);

            var group = new Group
            {
                Name = dto.Name
            };

            _groupRepository.Add(group);
            _unitOfWork.Complete();
        }

        public void EditGroupName(int groupId, EditGroupNameDto dto)
        {
            var group = _groupRepository.GetGroupById(groupId);

            StopIfGroupNotFound(group);

            StopIfGroupNameIsDuplicated(groupId, dto.Name);

            group.Name = dto.Name;
            _unitOfWork.Complete();
        }

        public void Delete(int groupId)
        {
            var group = _groupRepository
                .GetGroupById(groupId);

            StopIfGroupNotFound(group);

            StopIfGroupHasProduct(group);


            _groupRepository.Remove(group);
            _unitOfWork.Complete();
        }

        public GetOneGroupWithProductsDto GetOneGroupWithProducts(int groupId)
        {
            return _groupRepository.GetOneGroupWithProductsByGroupId(groupId);
        }

        public HashSet<GetAllGroupsDto> GetAllGroups()
        {
            return _groupRepository.GetAllGroups();
        }


        private void StopIfGroupHasProduct(Group group)
        {
            if (group.Products.Count() != 0)
            {
                throw new GroupHasProductsException();
            }
        }
        private void StopIfGroupNotFound(Group group)
        {
            if (group == null)
            {
                throw new GroupNotFoundException();
            }
        }
        private void StopIfGroupNameIsDuplicated(string name)
        {
            var isDuplicateName = _groupRepository.IsDuplicatedName(name);
            if (isDuplicateName)
            {
                throw new GroupNameIsDuplicatedException();
            }
        }
        private void StopIfGroupNameIsDuplicated(int groupId,string name)
        {
            var isDuplicateName = _groupRepository
                .IsDuplicatedNameExceptThisGroup(groupId, name);

            if (isDuplicateName)
            {
                throw new GroupNameIsDuplicatedException();
            }
        }


    }
}
