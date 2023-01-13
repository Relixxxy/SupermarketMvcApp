namespace SupermarketApp.Data.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
