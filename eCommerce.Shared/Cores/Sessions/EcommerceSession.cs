namespace eCommerce.Shared.Cores.Sessions;

public class EcommerceSession : IEcommerceSession
{
    public int? UserId { get; }
    public string RoleName { get; }
}