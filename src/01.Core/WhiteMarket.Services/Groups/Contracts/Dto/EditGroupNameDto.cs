using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteMarket.Services.Groups.Contracts.Dto
{
    public class EditGroupNameDto
    {
        [Required,MaxLength(50)]
        public string Name { get; set; }
    }
}
