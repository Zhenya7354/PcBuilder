using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IUserContextAccessor
{
    public UserContextResponse GetUserContext();
}
