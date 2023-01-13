using Microsoft.AspNetCore.Http;
using SupermarketApp.Data.Entities;

namespace SupermarketApp.Data.Models
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public IFormFile? ImageFile { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
