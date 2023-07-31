namespace WhiteMarket.Entities
{
    public class Group
    {
        public Group()
        {
            Products = new();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public HashSet<Product> Products { get; set; }
    }
}