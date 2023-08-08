using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Services.Groups.Contracts.Dto;

namespace WhiteMarket.TestTools.Groups
{
    public static class EditGroupNameFactory
    {
        public static EditGroupNameDto Generate(string name = "dummy")
        {
            return new EditGroupNameDto
            {
                Name = name
            };

        }
    }
}
