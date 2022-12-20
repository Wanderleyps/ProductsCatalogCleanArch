using MediatR;
using ProductsCatalogCleanArch.Domain.Entities;
using System.Collections.Generic;

namespace ProductsCatalogCleanArch.Application.Products.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
