using eCommerce.Shared.Cores.DataFilters;
using eCommerce.Shared.DataTransferObjects.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Shared.Queries.Users
{
    public class GetAllUserPagingQuery : GridParam, IRequest<PagingBase<UserPageDto>> { }
}
