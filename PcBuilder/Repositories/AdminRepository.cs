using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class AdminRepository(PcDbContext context) : IAdminRepository
{
    private readonly PcDbContext _context = context;
    public async Task<List<AdminEntity>> GetAdminsAsync()
    {
        return await _context.Admin.Include(a => a.User).ToListAsync();
    }
    public async Task<AdminEntity?> GetAdminByIdAsync(int id)
    {
        var admin = await _context.Admin.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id) ??
            throw new KeyNotFoundException("Admin not found");
        return admin;
    }

    public async Task AddAdmin(AdminEntity admin)
    {
        _context.Admin.Add(admin);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAdmin(AdminEntity admin)
    {
        _context.Admin.Remove(admin);
        await _context.SaveChangesAsync();
    }


    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
