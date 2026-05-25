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
        return await _context.HardDrive.Include(h => h.Brand).ToListAsync();
    }

    public async Task<HardDriveEntity?> GetHardDriveByIdAsync(int id)
    {
        return await _context.HardDrive.Include(h => h.Brand).FirstOrDefaultAsync(h => h.Id == id);
    }

    public Task AddHardDriveAsync(HardDriveEntity hardDrive)
    {
        _context.HardDrive.Add(hardDrive);
        return Task.CompletedTask;
    }

    public Task DeleteHardDriveAsync(HardDriveEntity hardDrive)
    {
        _context.HardDrive.Remove(hardDrive);
        return Task.CompletedTask;
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
