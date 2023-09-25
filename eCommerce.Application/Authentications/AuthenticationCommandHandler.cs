using eCommerce.Domain.Domains.Users;
using eCommerce.EntityFrameworkCore.Entities;
using eCommerce.Shared.Commands.Authentications;
using eCommerce.Shared.DataTransferObjects.Authentications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Authentications;

public class AuthenticationCommandHandler 
    : IRequestHandler<RegisterAccountCommand,bool>,
        IRequestHandler<LoginAdminSiteCommand,LoginDto>
{
    private readonly UserDomain _userDomain;
    private readonly SignInDomain _signInDomain;
    public AuthenticationCommandHandler(
        UserDomain userDomain,
        SignInDomain signInDomain
    )
    {
        _userDomain = userDomain;
        _signInDomain = signInDomain;
    }
    public async Task<bool> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        var existAccount = await _userDomain.Users
            .Where(x => x.UserName == request.Username || x.Email == request.Email ||
                        x.PhoneNumber == request.PhoneNumber)
            .Where(x => !x.IsAdmin)
            .FirstOrDefaultAsync();

        if (existAccount != null)
            return false;

        await _userDomain.CreateAsync(new User()
        {
            Email = request.Email,
            IsAdmin = false,
            Address = request.Address,
            UserName = request.Username,
            PhoneNumber = request.PhoneNumber
        }, request.Password);

        return true;
    }

    public async Task<LoginDto> Handle(LoginAdminSiteCommand request, CancellationToken cancellationToken)
    {
        var isLogged = await _signInDomain.PasswordSignInAsync(new User()
        {
            UserName = request.UserName,
            IsAdmin = true
        }, request.Password, false, true);

        if (!isLogged.Succeeded)
        {
            var user = await _userDomain.FindByNameAsync(request.UserName);
            return new LoginSucess(user.Id, user.UserName, user.Email, user.Address);
        }

        return new LoginFail();
    }
}