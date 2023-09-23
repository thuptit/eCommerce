using eCommerce.Shared.Commands.Authentications;
using MediatR;

namespace eCommerce.Application.Authentications;

public class AuthenticationCommandHandler : IRequestHandler<RegisterAccountCommand,bool>
{
    public Task<bool> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}