using eCommerce.Shared.DataTransferObjects.Categories;
using MediatR;

namespace eCommerce.Shared.Queries.Categories;

public record GetAllCategoryQuery : IRequest<IEnumerable<CategoryDto>>
{
}