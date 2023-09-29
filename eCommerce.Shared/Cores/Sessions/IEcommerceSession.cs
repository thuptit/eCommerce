namespace eCommerce.Shared.Cores.Sessions;

public interface IEcommerceSession
{
    int? UserId { get; }
    string RoleName { get; }
}