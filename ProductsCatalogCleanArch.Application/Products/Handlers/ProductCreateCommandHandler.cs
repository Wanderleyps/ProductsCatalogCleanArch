using MediatR;
using ProductsCatalogCleanArch.Application.Products.Commands;
using ProductsCatalogCleanArch.Domain.Entities;
using ProductsCatalogCleanArch.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProductsCatalogCleanArch.Application.Products.Handlers
{
    //informa qual o command que vai porcessar e o tipo de retorno (Product)
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        public ProductCreateCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new
                ArgumentNullException(nameof(productRepository));
        }
        public async Task<Product> Handle(ProductCreateCommand request,
            CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.Description, request.Price,
                              request.Stock, request.Image);
            
            //Como a entidade product realiza validação dos dados é necessário verificar se as mesmas passaram
            if (product == null)
            {
                throw new ApplicationException($"Error creating entity.");
            }
            else
            {
                product.CategoryId = request.CategoryId;
                return await _productRepository.CreateAsync(product);
            }
        }
    }
}
