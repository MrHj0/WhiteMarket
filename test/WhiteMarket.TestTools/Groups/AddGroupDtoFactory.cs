using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Services.Groups.Contracts.Dto;

namespace WhiteMarket.TestTools.Groups
{
    public static class AddGroupDtoFactory
    {
        public static AddGroupDto Generate(string name = "dummy")
        {
            return new AddGroupDto
            {
                Name = name
            };
        }
    }
}
