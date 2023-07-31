using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;

namespace WhiteMarket.TestTools.Groups
{
    public static class GroupFactory
    {
        public static Group Generate(string name = "dummy")
        {
            return new Group
            {
                Name = name
            };
        }
    }
}
