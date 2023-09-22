using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using eCommerce.EntityFrameworkCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace eCommerce.EntityFrameworkCore;

public class eCommerceDbContext : IdentityDbContext<User,Role,long>
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public eCommerceDbContext(DbContextOptions options) : base(options)
    {
    }
}