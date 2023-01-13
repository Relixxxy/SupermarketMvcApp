namespace SupermarketApp.Data.Entities
{
    public class Manufacturer : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
