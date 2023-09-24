using MediatR;

namespace eCommerce.Shared.Commands.Authentications;

public class RegisterAccountCommand : IRequest<bool>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
}