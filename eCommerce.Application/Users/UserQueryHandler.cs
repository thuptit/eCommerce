using eCommerce.Domain.Repositories;
using eCommerce.EntityFrameworkCore.Entities;
using eCommerce.Shared.Commands.Categories;
using eCommerce.Shared.Cores.DataFilters;
using eCommerce.Shared.DataTransferObjects.Users;
using eCommerce.Shared.Queries.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Users
{
    public class UserQueryHandler : ApplicationServiceBase, IRequestHandler<GetAllUserPagingQuery, PagingBase<UserPageDto>>
    {
        private readonly IRepository<User, long> _userRepo;
        public UserQueryHandler(IRepository<User, long> userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<PagingBase<UserPageDto>> Handle(GetAllUserPagingQuery request, CancellationToken cancellationToken)
        {
            var query = _userRepo.GetAll()
                .Select(x => new UserPageDto
                {
                    Id = x.Id,
                    Address = x.Address,
                    AvatarUrl = x.AvatarUrl,
                    UserName = x.UserName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    CreationTime = x.CreationTime
                });
            return await query.GetPagingResultAsync(request);
        }
    }
}
