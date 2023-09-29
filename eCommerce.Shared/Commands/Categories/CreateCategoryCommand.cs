using MediatR;

namespace eCommerce.Shared.Commands.Categories;

public class CreateCategoryCommand : IRequest<bool>
{
    public string Name { get; set; }
}