namespace SupermarketApp.Models
{
    public class Manufacturer : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
