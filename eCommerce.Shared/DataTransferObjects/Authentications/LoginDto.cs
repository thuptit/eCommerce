namespace eCommerce.Shared.DataTransferObjects.Authentications;

public class LoginDto
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public IList<string> Roles { get; set; }
    public bool IsSucess { get; set; }
    public IEnumerable<string> ErrorMessages { get; set; }
}

public class LoginSucess : LoginDto
{
    public LoginSucess(long id, string userName, string email, string address, IList<string> roles)
    {
        Id = id;
        UserName = userName;
        Email = email;
        Address = address;
        IsSucess = true;
        Roles = roles;
    }
}

public class LoginFail : LoginDto
{
    public LoginFail(IEnumerable<string> errorMessages = null)
    {
        IsSucess = false;
        ErrorMessages = errorMessages;
    }
}