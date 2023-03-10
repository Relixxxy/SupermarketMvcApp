using Microsoft.AspNetCore.Http;
using SupermarketApp.Data.Entities;

namespace SupermarketApp.Core.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string? Image { get; set; }
        public IFormFile? ImageFile { get; set; }
        public DepartmentModel? Department { get; set; }
        public ManufacturerModel? Manufacturer { get; set; }
        public int ManufacturerId { get; set; }
        public int? DepartmentId { get; set; }

        public override bool Equals(object? obj)
        {
            var product = obj as ProductModel;
            return Id.Equals(product.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
