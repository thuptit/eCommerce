using eCommerce.Domain.Domains;
using eCommerce.Shared.Queries.Categories;
using eCommerce.Shared.DataTransferObjects.Categories;
using MediatR;

namespace eCommerce.Application.Categories;

public class CategoryQueryHandler : IRequestHandler<GetAllCategoryQuery,IEnumerable<CategoryDto>>
{
    private readonly CategoryDomain _categoryDomain;
    public CategoryQueryHandler(CategoryDomain categoryDomain)
    {
        _categoryDomain = categoryDomain;
    }
    public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _categoryDomain.GetAll();
    }
}