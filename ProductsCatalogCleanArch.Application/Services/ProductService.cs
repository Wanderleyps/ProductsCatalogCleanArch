using AutoMapper;
using MediatR;
using ProductsCatalogCleanArch.Application.DTOs;
using ProductsCatalogCleanArch.Application.Interfaces;
using ProductsCatalogCleanArch.Application.Products.Commands;
using ProductsCatalogCleanArch.Application.Products.Queries;
using ProductsCatalogCleanArch.Domain.Entities;
using ProductsCatalogCleanArch.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsCatalogCleanArch.Application.Services
{   
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;//utiliza essa instância para enviar os requests para os handles
        public ProductService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productsQuery = new GetProductsQuery();

            if (productsQuery == null)
                throw new Exception($"Entity could not be loaded.");

            //envia-se o tipo de handler através do .Send para ser executado
            var result = await _mediator.Send(productsQuery);

            //necessário mapear o retorno para DTO
            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task<ProductDTO> GetById(int? id)
        {
            var productByIdQuery = new GetProductByIdQuery(id.Value);

            if (productByIdQuery == null)
                throw new Exception($"Entity could not be loaded.");

            var result = await _mediator.Send(productByIdQuery);

            return _mapper.Map<ProductDTO>(result);
        }

        public async Task<ProductDTO> GetProductCategory(int? id)
        {
            var productByIdQuery = new GetProductByIdQuery(id.Value);

            if (productByIdQuery == null)
                throw new Exception($"Entity could not be loaded.");

            var result = await _mediator.Send(productByIdQuery);

            return _mapper.Map<ProductDTO>(result);
        }

        public async Task Add(ProductDTO productDto)
        {
            //realiza o mapeamento da classe DTO para classe command
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDto);
            //através do tipo de classe command informada o mediator sabe qual handler chamar
            await _mediator.Send(productCreateCommand);
        }

        public async Task Update(ProductDTO productDto)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDto);
            await _mediator.Send(productUpdateCommand);
        }

        public async Task Remove(int? id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id.Value);
            if (productRemoveCommand == null)
                throw new Exception($"Entity could not be loaded.");

            await _mediator.Send(productRemoveCommand);
        }
    }
}
