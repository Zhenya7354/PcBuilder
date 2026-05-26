using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class HardDriveRepository(PcDbContext context) : IHardDriveRepository
{
    private readonly PcDbContext _context = context;

    public async Task<List<HardDriveEntity>> GetAllHardDrivesAsync()
    {
        return await _context.HardDrive.Include(h => h.Brand).AsNoTracking().ToListAsync();
    }

    public async Task<HardDriveEntity?> GetHardDriveByIdAsync(int id)
    {
        return await _context.HardDrive.Include(h => h.Brand).AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task AddHardDriveAsync(HardDriveEntity hardDrive)
    {
        await _context.HardDrive.AddAsync(hardDrive);
    }

    public async Task DeleteHardDriveAsync(HardDriveEntity hardDrive)
    {
        await _context.HardDrive.Where(h => h.Id == hardDrive.Id).ExecuteDeleteAsync();
    }

    public async Task<bool> BrandExistsAsync(int brandId)
    {
        return await _context.Brand.AnyAsync(b => b.Id == brandId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
