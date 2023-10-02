namespace eCommerce.EntityFrameworkCore.UnitOfWorks;

public sealed class UnitOfWorkAttribute : Attribute
{
    public bool isTransactional { get; set; } = false;

    public UnitOfWorkAttribute()
    {
        isTransactional = true;
    }
    public UnitOfWorkAttribute(bool isTransactional)
    {
        this.isTransactional = isTransactional;
    }
}