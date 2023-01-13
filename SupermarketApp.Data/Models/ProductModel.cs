using Microsoft.AspNetCore.Http;
using SupermarketApp.Data.Entities;

namespace SupermarketApp.Data.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string? Image { get; set; }
        public IFormFile? ImageFile { get; set; }
        public Department? Department { get; set; }
        public Manufacturer? Manufacturer { get; set; }
    }
}
