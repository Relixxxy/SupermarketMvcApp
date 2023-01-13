using Microsoft.EntityFrameworkCore;
using SupermarketApp.Data.Context;
using SupermarketApp.Data.Entities;
using SupermarketApp.Data.Repository.Interfaces;

namespace SupermarketApp.Data.Repository
{
    public class DepartmentRepository : IRepository<Department>
    {
        private readonly SupermarketContext _context;
        public DepartmentRepository(SupermarketContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            _context.SaveChanges();
        }

        public async Task<Department> FindByIdAsync(int id)
        {
            var department = await _context.Departments.AsNoTracking().Include(d => d.Products).FirstOrDefaultAsync(d => d.Id == id);
            return department;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            var departments = await _context.Departments.Include(d => d.Products).ToListAsync();
            return departments;
        }

        public async Task RemoveAsync(Department department)
        {
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }
    }
}
