using eCommerce.Domain.Domains;
using eCommerce.Shared.Commands.Categories;
using MediatR;

namespace eCommerce.Application.Categories;

public class CategoryCommandHandler : IRequestHandler<CreateCategoryCommand,bool>
{
    private readonly CategoryDomain _categoryDomain;

    public CategoryCommandHandler(CategoryDomain categoryDomain)
    {
        _categoryDomain = categoryDomain;
    }
    public async Task<bool> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        await _categoryDomain.Create(request);
        return true;
    }
}