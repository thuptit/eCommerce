namespace eCommerce.Shared.DataTransferObjects.Authentications;

public class LoginResponseDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public long Id { get; set; }
    public string AccessToken { get; set; }
}