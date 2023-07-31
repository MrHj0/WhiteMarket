using WhiteMarket.Entities;
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

        public void Delete(int groupId)
        {
            var group = _groupRepository
                .GetGroupById(groupId);

            if(group.Products.Count() != 0)
            {
                throw new GroupHasProductsException();
            }


            _groupRepository.Delete(group);
            _unitOfWork.Complete();
        }

        private void StopIfGroupNameIsDuplicated(string name)
        {
            var isDuplicateName = _groupRepository.IsDuplicatedName(name);
            if (isDuplicateName)
            {
                throw new GroupNameIsDuplicatedException();
            }
        }
    }
}
