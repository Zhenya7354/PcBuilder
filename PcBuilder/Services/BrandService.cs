using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Dtos;
using PcBuilder.Models;

namespace PcBuilder.Services
{
    public class BrandService(PcDbContext context)
    {
        private readonly PcDbContext _context = context;

        public async Task<List<Brand>> GetAllBrandsAsync()
        {
            return await _context.Brand.ToListAsync();
        }

        public async Task<Brand> GetBrandByIdAsync(int id)
        {
            var brand = await _context.Brand.FirstOrDefaultAsync(b => b.id == id);
            if (brand is null)
            {
                throw new ArgumentException($"Brand with ID {id} not found.");
            }

            return brand;
        }

        public async Task<Brand> AddBrandAsync(BrandCreateDto dto)
        {
            var brand = new Brand { name = dto.Name };
            _context.Brand.Add(brand);
            await _context.SaveChangesAsync();
            return brand;
        }

        public async Task<Brand> UpdateBrandAsync(int id, BrandUpdateDto dto)
        {
            var brand = await _context.Brand.FindAsync(id);
            if (brand is null)
                throw new ArgumentException($"Brand with ID {id} not found.");

            if (!string.IsNullOrWhiteSpace(dto.Name)) brand.name = dto.Name;

            await _context.SaveChangesAsync();
            return brand;
        }

        public async Task DeleteBrandAsync(int id)
        {
            var brand = await _context.Brand.FindAsync(id);
            if (brand is null)
                throw new ArgumentException($"Brand with ID {id} not found.");

            _context.Brand.Remove(brand);
            await _context.SaveChangesAsync();
        }
    }
}
