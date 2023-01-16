using Microsoft.EntityFrameworkCore;
using SupermarketApp.Data.Context;
using SupermarketApp.Data.Entities;
using SupermarketApp.Data.Repository.Interfaces;

namespace SupermarketApp.Data.Repository
{
    public class ManufacturerRepository : IRepository<Manufacturer>
    {
        private readonly SupermarketContext _context;
        public ManufacturerRepository(SupermarketContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Manufacturer manufacturer)
        {
            await _context.Manufacturers.AddAsync(manufacturer);
            _context.SaveChanges();
        }

        public async Task<Manufacturer> FindByIdAsync(int id)
        {
            var manufacturer = await _context.Manufacturers.AsNoTracking().Include(m => m.Products).FirstOrDefaultAsync(d => d.Id == id);
            return manufacturer;
        }

        public async Task<IEnumerable<Manufacturer>> GetAllAsync()
        {
            var departments = await _context.Manufacturers.Include(d => d.Products).ToListAsync();
            return departments;
        }

        public async Task RemoveAsync(Manufacturer manufacturer)
        {
            _context.Manufacturers.Remove(manufacturer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Manufacturer manufacturer)
        {
            _context.Manufacturers.Update(manufacturer);
            await _context.SaveChangesAsync();
        }
    }
}
