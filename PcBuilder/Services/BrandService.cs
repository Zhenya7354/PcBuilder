using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Models;
using PcBuilder.Entities;

namespace PcBuilder.Services;

public class BrandService(PcDbContext context)
{
    private readonly PcDbContext _context = context;

    public async Task<List<BrandEntity>> GetAllBrandsAsync()
    {
        return await _context.Brand.ToListAsync();
    }

    public async Task<BrandEntity> GetBrandByIdAsync(int id)
    {
        var brand = await _context.Brand.FirstOrDefaultAsync(b => b.Id == id);
        if (brand is null)
        {
            throw new KeyNotFoundException($"Brand with ID {id} not found.");
        }

        return brand;
    }

    public async Task<BrandEntity> AddBrandAsync(BrandCreate dto)
    {
        var brand = new BrandEntity { Name = dto.Name };
        _context.Brand.Add(brand);
        await _context.SaveChangesAsync();
        return brand;
    }

    public async Task<BrandEntity> UpdateBrandAsync(int id, BrandUpdate dto)
    {
        var brand = await _context.Brand.FindAsync(id);
        if (brand is null)
            throw new KeyNotFoundException($"Brand with ID {id} not found.");

        if (!string.IsNullOrWhiteSpace(dto.Name)) brand.Name = dto.Name;

        await _context.SaveChangesAsync();
        return brand;
    }

    public async Task DeleteBrandAsync(int id)
    {
        var brand = await _context.Brand.FindAsync(id);
        if (brand is null)
            throw new KeyNotFoundException($"Brand with ID {id} not found.");

        _context.Brand.Remove(brand);
        await _context.SaveChangesAsync();
    }
}
