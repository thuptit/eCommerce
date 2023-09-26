using System.Linq.Dynamic.Core;
using eCommerce.EntityFrameworkCore.Entities;
using eCommerce.Shared;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.EntityFrameworkCore.Seeds;

public class SeedData
{
    private readonly eCommerceDbContext _context;
    private readonly UserManager<User> _userManager;
    public SeedData(eCommerceDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public static void UserRoleSeed(eCommerceDbContext dbContext)
    {
        var isExisted = dbContext.Users
            .Where(x => x.UserName == eCommerceConsts.UserNameAdmin && !x.IsDeleted && x.IsAdmin)
            .Any();
        if (!isExisted)
        {
            var user = new User()
            {
                UserName = eCommerceConsts.UserNameAdmin,
                Email = "admin@thunv.com",
                IsAdmin = true
            };
            var passwordHash = new PasswordHasher<User>();
            var passwordAfterHash = passwordHash.HashPassword(user,eCommerceConsts.PasswordAdmin);
            user.PasswordHash = passwordAfterHash;
            
            
        }
    }
}