using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class BrandRepository(PcDbContext context) : IBrandRepository
{
    private readonly PcDbContext _context = context;

    public async Task<List<BrandEntity>> GetAllBrandsAsync(CancellationToken cancellationToken)
    {
        return await _context.Brand.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<BrandEntity?> GetBrandByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Brand.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task AddBrandAsync(BrandEntity brand, CancellationToken cancellationToken)
    {
        await _context.Brand.AddAsync(brand, cancellationToken);
    }

    public async Task DeleteBrandAsync(BrandEntity brand, CancellationToken cancellationToken)
    {
        await _context.Brand.Where(b => b.Id == brand.Id).ExecuteDeleteAsync(cancellationToken); 
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
