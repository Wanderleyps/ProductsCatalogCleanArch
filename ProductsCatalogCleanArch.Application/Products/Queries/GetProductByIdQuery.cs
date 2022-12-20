using MediatR;
using ProductsCatalogCleanArch.Domain.Entities;
using System.Collections.Generic;

namespace ProductsCatalogCleanArch.Application.Products.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}
