using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class HardDriveRepository(PcDbContext context) : IHardDriveRepository
{
    private readonly PcDbContext _context = context;

    public async Task<List<HardDriveEntity>> GetAllHardDrivesAsync(CancellationToken cancellationToken)
    {
        return await _context.HardDrive.Include(h => h.Brand).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<HardDriveEntity?> GetHardDriveByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.HardDrive.Include(h => h.Brand).AsNoTracking().FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
    }

    public async Task AddHardDriveAsync(HardDriveEntity hardDrive, CancellationToken cancellationToken)
    {
        await _context.HardDrive.AddAsync(hardDrive, cancellationToken);
    }

    public async Task DeleteHardDriveAsync(HardDriveEntity hardDrive, CancellationToken cancellationToken)
    {
        await _context.HardDrive.Where(h => h.Id == hardDrive.Id).ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken)
    {
        return await _context.Brand.AnyAsync(b => b.Id == brandId, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
