using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteMarket.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MinimumInventory { get; set; }
        public int Inventory { get; set; }
        public int GroupId { get; set; }
        public InventoryStatus Status { get; set; }

        public Group Group { get; set; }
    }

    public enum InventoryStatus
    {
        UnAvalable = 1,
        LowInventory = 2,
        Avalable = 3
    }
}
