using MediatR;
using ProductsCatalogCleanArch.Domain.Entities;

namespace ProductsCatalogCleanArch.Application.Products.Commands
{
    //classe não precisa herdar de ProductCommand, pois para esta operação só necessita do id.
    public class ProductRemoveCommand : IRequest<Product>
    {
        public int Id { get; set; }

        public ProductRemoveCommand(int id)
        {
            Id = id;
        }
    }
}
