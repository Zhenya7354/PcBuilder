using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IRegularUserRepository
{
    public Task<List<RegularUserEntity>> GetRegularUsers();
    
    public Task<RegularUserEntity?> GetRegularUserByIdAsync(int id);

}
