using Microsoft.EntityFrameworkCore.Storage;
using PcBuilder.Data;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class TransactionService(PcDbContext context) : ITransactionService
{
    private readonly PcDbContext _context = context;
    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return _context.Database.BeginTransactionAsync();
    }
}
