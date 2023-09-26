using System.Linq.Dynamic.Core;
using eCommerce.EntityFrameworkCore.Entities;
using eCommerce.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace eCommerce.EntityFrameworkCore.Seeds;

public class SeedData
{
    private readonly eCommerceDbContext _context;
    public SeedData(eCommerceDbContext context)
    {
        _context = context;
    }
    public async Task UserRoleSeed()
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                string[] roles = new string[] { eCommerceConsts.RoleAdmin, eCommerceConsts.RoleStaff };
                foreach (string role in roles)
                {
                    if (!_context.Roles.Any(r => r.Name == role))
                    {
                        _context.Roles.Add(new Role()
                        {
                            Name = role,
                            NormalizedName = role.Normalize()
                        });
                    }
                }

                await _context.SaveChangesAsync();
                
                var isExisted = _context.Users
                    .Where(x => x.UserName == eCommerceConsts.UserNameAdmin && !x.IsDeleted && x.IsAdmin)
                    .Any();
                if (!isExisted)
                {
                    var user = new User()
                    {
                        UserName = eCommerceConsts.UserNameAdmin,
                        Email = "admin@thunv.com",
                        IsAdmin = true,
                        Address = "Ha Noi, Viet Nam"
                    };
                    var passwordHash = new PasswordHasher<User>();
                    var passwordAfterHash = passwordHash.HashPassword(user,eCommerceConsts.PasswordAdmin);
                    user.PasswordHash = passwordAfterHash;

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    _context.UserRoles.Add(new IdentityUserRole<long>()
                    {
                        RoleId = _context.Roles.FirstOrDefault().Id,
                        UserId = user.Id
                    });
                }

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
            }
        }
        
    }
}