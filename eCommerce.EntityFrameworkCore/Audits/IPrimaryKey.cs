namespace eCommerce.EntityFrameworkCore.Audits;

public interface IPrimaryKey<TKey>
{
    public TKey Id { get; set; }
}