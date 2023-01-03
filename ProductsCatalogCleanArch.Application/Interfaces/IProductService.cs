using ProductsCatalogCleanArch.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsCatalogCleanArch.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetById(int? id);
        Task Add(ProductDTO productDto);
        Task Update(ProductDTO productDto);
        Task Remove(int? id);
    }
}
