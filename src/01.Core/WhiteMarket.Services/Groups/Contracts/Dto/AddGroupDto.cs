using System.ComponentModel.DataAnnotations;

namespace WhiteMarket.Services.Groups.Contracts.Dto
{
    public class AddGroupDto
    {
        [Required,MaxLength(50)]
        public string Name { get; set; }
    }
}
