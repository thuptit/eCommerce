using eCommerce.Application.Users.Dtos;
using eCommerce.Domain.Repositories;
using eCommerce.EntityFrameworkCore.Entities;
using eCommerce.Shared.DataTransferObjects.Users;
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

    public async Task<List<AutoCompleteUserDto>> GetAllUser()
    {
        return await _userRepository.GetAll().Select(x => new AutoCompleteUserDto()
        {
            UserName = x.UserName,
            Id = x.Id
        }).ToListAsync();
    }

    public async Task<UserPageDto> GetUserInfo(long id)
    {
        return await _userRepository.GetAll()
            .Where(x => x.Id == id)
            .Select(x => new UserPageDto()
            {
                Id = x.Id,
                Address = x.Address,
                AvatarUrl = x.AvatarUrl,
                UserName = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                CreationTime = x.CreationTime
            }).FirstAsync();
    }
}