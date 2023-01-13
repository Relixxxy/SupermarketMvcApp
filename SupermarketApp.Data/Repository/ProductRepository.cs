using Microsoft.EntityFrameworkCore;
using SupermarketApp.Data.Context;
using SupermarketApp.Data.Entities;
using SupermarketApp.Data.Repository.Interfaces;

namespace SupermarketApp.Data.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly SupermarketContext _context;
        public ProductRepository(SupermarketContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            _context.SaveChanges();
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            var product = await _context.Products.AsNoTracking().Include(p => p.Manufacturer).Include(p => p.Department).FirstOrDefaultAsync(d => d.Id == id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await _context.Products.Include(p => p.Manufacturer).Include(p => p.Department).ToListAsync();
            return products;
        }

        public async Task RemoveAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
