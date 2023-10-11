using MediatR;

namespace eCommerce.Shared.Commands.Categories;

public class CreateCategoryCommand : IRequest<string>
{
    public string Name { get; set; }
    public string Description { get; set; }
}