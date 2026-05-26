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
        return await _context.Brand.AsNoTracking().ToListAsync();
    }

    public async Task<BrandEntity?> GetBrandByIdAsync(int id)
    {
        return await _context.Brand.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task AddBrandAsync(BrandEntity brand)
    {
        await _context.Brand.AddAsync(brand);
    }

    public async Task DeleteBrandAsync(BrandEntity brand)
    {
        await _context.Brand.Where(b => b.Id == brand.Id).ExecuteDeleteAsync(); 
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
