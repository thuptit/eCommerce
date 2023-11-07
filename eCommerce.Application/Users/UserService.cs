using eCommerce.Application.Users.Dtos;
using eCommerce.Domain.Repositories;
using eCommerce.EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Users;

public class UserService : IUserService
{
    private readonly IRepository<User, long> _userRepository;
    public UserService(IRepository<User, long> userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<List<AutoCompleteUserDto>> GetUserByName(string name)
    {
        return await _userRepository.GetAll()
            .Where(x => x.UserName.Contains(name))
            .Select(x => new AutoCompleteUserDto()
            {
                Id = x.Id,
                UserName = x.UserName
            })
            .ToListAsync();
    }
}