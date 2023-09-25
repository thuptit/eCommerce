using eCommerce.Shared.DataTransferObjects.Authentications;
using MediatR;

namespace eCommerce.Shared.Commands.Authentications;

public class LoginAdminSiteCommand : IRequest<LoginDto>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}