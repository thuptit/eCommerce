namespace eCommerce.Shared.DataTransferObjects.Authentications;

public class LoginResponseDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public long UserId { get; set; }
    public string AccessToken { get; set; }
    public IList<string> Roles { get; set; }
    public string AvatarUrl { get; set; }
}