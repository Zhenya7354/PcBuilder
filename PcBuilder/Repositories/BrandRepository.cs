using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class BrandRepository(PcDbContext context) : IBrandRepository
{
    private readonly PcDbContext _context = context;

    public async Task<List<BrandEntity>> GetAllBrandsAsync()
    {
        return await _context.Brand.ToListAsync();
    }

    public async Task<BrandEntity?> GetBrandByIdAsync(int id)
    {
        return await _context.Brand.FirstOrDefaultAsync(b => b.Id == id);
    }

    public Task AddBrand(BrandEntity brand)
    {
        _context.Brand.Add(brand);
        return Task.CompletedTask;
    }

    public Task DeleteBrand(BrandEntity brand)
    {
        _context.Brand.Remove(brand);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
