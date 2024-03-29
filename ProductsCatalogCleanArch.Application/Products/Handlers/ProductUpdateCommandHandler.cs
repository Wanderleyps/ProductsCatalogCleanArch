﻿using MediatR;
using ProductsCatalogCleanArch.Application.Products.Commands;
using ProductsCatalogCleanArch.Domain.Entities;
using ProductsCatalogCleanArch.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProductsCatalogCleanArch.Application.Products.Handlers
{
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        public ProductUpdateCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ??//caso seja null, retorna uma exceção
            throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<Product> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
            {
                throw new ApplicationException($"Entity could not be found.");
            }

            else
            {
                product.Update(request.Name, request.Description, request.Price,
                                request.Stock, request.Image, request.CategoryId);
                return await _productRepository.UpdateAsync(product);
            }

        }
    }
}
