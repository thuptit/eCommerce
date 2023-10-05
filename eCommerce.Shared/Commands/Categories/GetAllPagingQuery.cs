using eCommerce.Shared.Cores.DataFilters;
using eCommerce.Shared.DataTransferObjects.Categories;
using MediatR;

namespace eCommerce.Shared.Commands.Categories;

public class GetAllPagingQuery : GridParam , IRequest<PagingBase<CategoryDto>>
{
}