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
        await _context.Admin.AddAsync(admin);
    }

    public async Task DeleteAdminAsync(AdminEntity admin)
    {
        await _context.Admin.Where(a => a.Id == admin.Id).ExecuteDeleteAsync();
    }


    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
