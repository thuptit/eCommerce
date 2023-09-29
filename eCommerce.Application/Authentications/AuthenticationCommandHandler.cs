using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using eCommerce.Domain.Domains.Users;
using eCommerce.EntityFrameworkCore.Entities;
using eCommerce.Shared.Commands.Authentications;
using eCommerce.Shared.DataTransferObjects.Authentications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace eCommerce.Application.Authentications;

public class AuthenticationCommandHandler 
    : IRequestHandler<RegisterAccountCommand,bool>,
        IRequestHandler<LoginAdminSiteCommand,LoginDto>,
        IRequestHandler<TokenGenerationCommand,string>
{
    private readonly UserDomain _userDomain;
    private readonly SignInDomain _signInDomain;
    private readonly IConfiguration _configuration;
    public AuthenticationCommandHandler(
        UserDomain userDomain,
        SignInDomain signInDomain,
        IConfiguration configuration
    )
    {
        _userDomain = userDomain;
        _signInDomain = signInDomain;
        _configuration = configuration;
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
        var isLogged = await _signInDomain.PasswordSignInAsync(request.UserName, request.Password, false, false);

        if (isLogged.Succeeded)
        {
            var user = await _userDomain.FindByNameAsync(request.UserName);
            var roles = await _userDomain.GetRolesAsync(user);
            return new LoginSucess(user.Id, user.UserName, user.Email, user.Address, roles);
        }

        return new LoginFail();
    }

    public Task<string> Handle(TokenGenerationCommand request, CancellationToken cancellationToken)
    {
        var issuer = _configuration["JWTToken:Issuer"];
        var audience = _configuration["JWTToken:Audience"];
        var key = Encoding.ASCII.GetBytes(_configuration["JWTToken:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", request.data.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, request.data.UserName),
                new Claim(JwtRegisteredClaimNames.Email, request.data.Email),
                new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString()),
                new Claim("Role",string.Join(",",request.data.Roles))
            }),
            Issuer = issuer,
            Audience = audience,
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        return Task.FromResult(jwtToken);
    }
}