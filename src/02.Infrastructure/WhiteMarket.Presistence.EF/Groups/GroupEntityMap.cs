using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMarket.Entities;

namespace WhiteMarket.Presistence.EF.Groups
{
    public class GroupEntityMap : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> group)
        {
            group.ToTable("Groups");
            group.HasKey(_ => _.Id);
            group.Property(_ => _.Id).ValueGeneratedOnAdd();
            group.Property(_ => _.Name).IsRequired().HasMaxLength(50);
        }
    }
}
