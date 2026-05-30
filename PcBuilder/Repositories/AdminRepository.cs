using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class AdminRepository(PcDbContext context) : IAdminRepository
{
    private readonly PcDbContext _context = context;
    public async Task<List<AdminEntity>> GetAdminsAsync()
    {
        return await _context.Admin.Include(a => a.User).AsNoTracking().ToListAsync();
    }
    public async Task<AdminEntity?> GetAdminByIdAsync(int id)
    {
        return await _context.Admin.Include(a => a.User).AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddAdminAsync(AdminEntity admin)
    {
        _context.Admin.Add(admin);
    }

    public async Task DeleteAdminAsync(AdminEntity admin)
    {
        _context.Admin.Remove(admin);
    }


    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
