using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Domain.Domains;
using eCommerce.EntityFrameworkCore.Entities;
using eCommerce.Shared;
using eCommerce.Shared.Commands.Users;
using eCommerce.Shared.Cores.DependencyInjections;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Application.Users
{
    public class UserCommandHandler : ApplicationServiceBase,
        IRequestHandler<CreateUserCommand,string>
    {
        private readonly UserDomain _userDomain;
        private readonly ConfigurationService _configurationService;
        public UserCommandHandler(
            UserDomain userDomain, 
            ConfigurationService configurationService
        )
        {
            _userDomain = userDomain;
            _configurationService = configurationService;
        }
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            string avatarUrl = _configurationService.GetDefaultAvatar();
            string fileName = request.UserName + "_img_" + Path.GetExtension(request.AvatarFile.FileName);
            if (request.AvatarFile != null)
            {
                var fullPath = Path.Combine(_configurationService.GetRootFolder(), "avatars", fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await request.AvatarFile.CopyToAsync(stream);
                    avatarUrl = _configurationService.GetHost() + "avatars/"+ fileName;
                }
            }
            var result = await _userDomain.CreateAsync(new User()
            {
                Email = request.Email,
                Address = request.Address,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                AvatarUrl = avatarUrl
            }, eCommerceConsts.PasswordAdmin);
            
            if (!result.Succeeded)
                throw new Exception("Create User Failed");
            
            var user = await _userDomain.FindByNameAsync(request.UserName);
            await _userDomain.AddToRoleAsync(user, eCommerceConsts.RoleAdmin);
            return "Create Successfully";
        }
    }
}
