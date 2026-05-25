namespace PcBuilder.Repositories.Interfaces;

public interface IAdminRepository
{
    public Task<List<AdminEntity>> GetAdminsAsync();
    public Task<AdminEntity?> GetAdminByIdAsync(int id);
    public Task AddAdmin(AdminEntity admin);
    public Task DeleteAdmin(AdminEntity admin);
    public Task SaveChangesAsync();
}
