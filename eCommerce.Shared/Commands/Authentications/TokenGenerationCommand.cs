using eCommerce.Shared.DataTransferObjects.Authentications;
using MediatR;

namespace eCommerce.Shared.Commands.Authentications;

public class TokenGenerationCommand : IRequest<string>
{
    public TokenGenerationCommand(LoginDto _data)
    {
        data = _data;
    }
    public LoginDto data { get; set; }
}