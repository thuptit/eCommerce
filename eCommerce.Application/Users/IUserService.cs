using eCommerce.Application.Users.Dtos;
using eCommerce.Shared.Cores.DependencyInjections;
using IScopedDependency = Volo.Abp.DependencyInjection.IScopedDependency;

namespace eCommerce.Application.Users;

public interface IUserService : IScopedDependency
{
    Task<List<AutoCompleteUserDto>> GetUserByName(string name);
}