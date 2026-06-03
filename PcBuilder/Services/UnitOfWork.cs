using Microsoft.EntityFrameworkCore.Storage;
using PcBuilder.Data;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly PcDbContext _context;

    public IAdminRepository AdminRepository { get; }

    public UnitOfWork(PcDbContext context, IAdminRepository adminRepository)
    {
        _context = context;
        AdminRepository = adminRepository;
    }
    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}
