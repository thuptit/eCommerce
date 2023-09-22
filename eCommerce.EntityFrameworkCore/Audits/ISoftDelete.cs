namespace eCommerce.EntityFrameworkCore.Audits;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
}