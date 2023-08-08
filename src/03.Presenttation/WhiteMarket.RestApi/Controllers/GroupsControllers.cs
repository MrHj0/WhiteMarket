using Microsoft.AspNetCore.Mvc;
using WhiteMarket.Services.Groups;
using WhiteMarket.Services.Groups.Contracts;
using WhiteMarket.Services.Groups.Contracts.Dto;

namespace WhiteMarket.RestApi.Controllers
{
    [Route("groups")]
    [ApiController]
    public class GroupsControllers : Controller
    {
        private readonly GroupService _service;

        public GroupsControllers(GroupService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Define([FromBody]AddGroupDto dto)
        {
            _service.Define(dto);
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete([FromRoute]int id)
        {
            _service.Delete(id);
        }

        [HttpPatch]
        [Route("{id}")]
        public void EditGroupName([FromRoute]int id, [FromBody]EditGroupNameDto dto)
        {
            _service.EditGroupName(id, dto);
        }

        [HttpGet]
        public HashSet<GetAllGroupsDto> GetAll()
        {
            return _service.GetAllGroups();
        }

        [HttpGet]
        [Route("{id}")]
        public GetOneGroupWithProductsDto GetOne([FromRoute]int id)
        {
            return _service.GetOneGroupWithProducts(id);
        }
    }
}
