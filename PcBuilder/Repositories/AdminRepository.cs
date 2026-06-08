using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class AdminRepository(PcDbContext context) : IAdminRepository
{
    private readonly PcDbContext _context = context;
    public async Task<List<AdminEntity>> GetAdminsAsync(CancellationToken cancellationToken)
    {
        return await _context.Admin.Include(a => a.User).AsNoTracking().ToListAsync(cancellationToken);
    }
    public async Task<AdminEntity?> GetAdminByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Admin.Include(a => a.User).AsNoTracking().FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task AddAdminAsync(AdminEntity admin, CancellationToken cancellationToken)
    {
        await _context.Admin.AddAsync(admin, cancellationToken);
    }

    public async Task DeleteAdminAsync(AdminEntity admin, CancellationToken cancellationToken)
    {
        await _context.Admin.Where(a => a.Id == admin.Id).ExecuteDeleteAsync(cancellationToken);
    }


    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
