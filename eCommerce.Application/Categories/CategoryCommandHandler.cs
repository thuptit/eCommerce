using eCommerce.Domain.Domains;
using eCommerce.Shared.Commands.Categories;
using MediatR;

namespace eCommerce.Application.Categories;

public class CategoryCommandHandler : IRequestHandler<CreateCategoryCommand,string>
{
    private readonly CategoryDomain _categoryDomain;

    public CategoryCommandHandler(CategoryDomain categoryDomain)
    {
        _categoryDomain = categoryDomain;
    }
    public async Task<string> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        await _categoryDomain.Create(request);
        return "Created Successfully";
    }
}