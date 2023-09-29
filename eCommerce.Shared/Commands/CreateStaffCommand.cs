using MediatR;

namespace eCommerce.Shared.Commands;

public class CreateStaffCommand : IRequest<bool>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
}