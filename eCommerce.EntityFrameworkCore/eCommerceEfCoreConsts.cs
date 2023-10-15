namespace eCommerce.EntityFrameworkCore;

public static class eCommerceDataFilter
{
    /// <summary>
    /// Prevents getting deleted data from database
    /// See <see cref="ISoftDelete"/> interface
    /// </summary>
    public const string SoftDelete = "SoftDelete";
}

public static class eCommerceAuditFields
{
    public const string CreatorId = "CreatorId";
    public const string ModifiorId = "ModifiorId";
    public const string DeletorId = "DeletorId";
}