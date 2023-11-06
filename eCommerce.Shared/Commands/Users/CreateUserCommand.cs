using MediatR;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Shared.Commands.Users;

public class CreateUserCommand : IRequest<string>
{
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public IFormFile AvatarFile { get; set; }
}