using eCommerce.Shared.DataTransferObjects.Authentications;
using MediatR;

namespace eCommerce.Shared.Commands.Authentications;

public class LoginAdminSiteWithGoogleCommand : IRequest<LoginDto>
{
    public string idToken { get; set; }
}