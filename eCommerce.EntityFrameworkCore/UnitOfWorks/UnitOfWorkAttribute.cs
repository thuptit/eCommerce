namespace eCommerce.EntityFrameworkCore.UnitOfWorks;

public sealed class UnitOfWorkAttribute : Attribute
{
    public bool isTransactional { get; set; } = true;
    
    public UnitOfWorkAttribute(){}
    public UnitOfWorkAttribute(bool isTransactional)
    {
        this.isTransactional = isTransactional;
    }
}