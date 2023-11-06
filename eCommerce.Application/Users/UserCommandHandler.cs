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
        private readonly IUserStore<User> _userStore;
        private readonly IUserRoleStore<User> _userRoleStore; 
        private readonly ConfigurationService _configurationService;
        public UserCommandHandler(
            UserDomain userDomain, 
            IUserStore<User> userStore,
            ConfigurationService configurationService,
            IUserRoleStore<User> userRoleStore)
        {
            _userDomain = userDomain;
            _userStore = userStore;
            _configurationService = configurationService;
            _userRoleStore = userRoleStore;
        }
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            string avatarUrl = _configurationService.GetDefaultAvatar();
            if (request.AvatarFile != null)
            {
                var fileName = Path.Combine(_configurationService.GetRootFolder(), "avatars", request.UserName + "_img_"+request.AvatarFile.FileName);
                using (var stream = new FileStream(fileName, FileMode.Create))
                {
                    await request.AvatarFile.CopyToAsync(stream);
                    avatarUrl = _configurationService.GetHost() + fileName;
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

            var user = await _userStore.FindByNameAsync(request.UserName,new CancellationToken());
            await _userRoleStore.AddToRoleAsync(user, eCommerceConsts.RoleAdmin,new CancellationToken());
            return "Create Successfully";
        }
    }
}
