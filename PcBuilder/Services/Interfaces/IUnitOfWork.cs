using Microsoft.EntityFrameworkCore.Storage;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Services.Interfaces;

public interface IUnitOfWork
{
    public IAdminRepository AdminRepository { get; }
    public Task Commit();
}
