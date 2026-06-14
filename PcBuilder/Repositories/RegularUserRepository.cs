using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Repositories;

public class RegularUserRepository(PcDbContext context) : IRegularUserRepository
{
    private readonly PcDbContext _context = context;
    public async Task AddRegularUserAsync(RegularUserEntity dto, CancellationToken cancellationToken)
    {
        await _context.AddAsync(dto, cancellationToken);
    }

    public async Task<RegularUserEntity?> GetRegularUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.RegularUser.Include(r => r.User).AsNoTracking().FirstOrDefaultAsync(u => u.Id == id,cancellationToken) ??
            throw new KeyNotFoundException("Regular user not found");
    }

    public async Task<List<RegularUserEntity>> GetRegularUsersAsync(CancellationToken cancellationToken)
    {
        return await _context.RegularUser.Include(r => r.User).AsNoTracking().ToListAsync(cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
