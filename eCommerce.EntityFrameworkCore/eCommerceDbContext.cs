using System.Linq.Expressions;
using System.Reflection;
using eCommerce.EntityFrameworkCore.Audits;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using eCommerce.EntityFrameworkCore.Entities;
using eCommerce.EntityFrameworkCore.Intercepters;
using eCommerce.EntityFrameworkCore.UnitOfWorks;
using eCommerce.Shared.Cores.Sessions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace eCommerce.EntityFrameworkCore;

public class eCommerceDbContext : IdentityDbContext<User,Role,long>, IUnitOfWork
{
    #region Tables
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<PaymentDetail> PaymentDetails { get; set; }
    public DbSet<ProductDiscount> ProductDiscounts { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductInventory> ProductInventories { get; set; }
    public DbSet<UserPayment> UserPayments { get; set; }
    #endregion
    
    private IDbContextTransaction _currentTransaction;
    private IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
    private readonly IEcommerceSession _session;
    private readonly ILogger<eCommerceDbContext> _logger;
    public eCommerceDbContext(DbContextOptions options, IEcommerceSession session) : base(options)
    {
        _session = session;
        _logger = NullLogger<eCommerceDbContext>.Instance;
        ;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()
            .AddInterceptors(new List<IInterceptor>()
            {
                new eCommerceSaveChangesIntercetor(_session?.UserId)
            });
        base.OnConfiguring(optionsBuilder);
    }

    #region custom implementation
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                entityType.AddSoftDeleteQuery();
            }
        }
    }
    
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            throw new Exception("Not initiate new transaction when DbContext is existing other transaction. " +
                                "Please commit transaction before create new transaction");
        }
        _currentTransaction = await Database.BeginTransactionAsync();
        return _currentTransaction;
    }

    public async Task CommitAsync()
    {
        if (_currentTransaction == null) 
            throw new InvalidOperationException("Current transaction should be not null");
        try
        {
            await SaveChangesAsync();
            await _currentTransaction.CommitAsync();
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            await RollbackAsync();
            
            async Task RollbackAsync()
            {
                try
                {
                    await _currentTransaction.RollbackAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                }
                
            }
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
    #endregion
}