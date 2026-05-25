using Microsoft.EntityFrameworkCore.Storage;

namespace PcBuilder.Services.Interfaces;

public interface ITransactionService
{
    public Task<IDbContextTransaction> BeginTransactionAsync();
}
