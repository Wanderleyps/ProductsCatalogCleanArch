using ProductsCatalogCleanArch.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsCatalogCleanArch.Domain.Interfaces
{
    interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCatories();
        Task<Category> GetById(int? id);
        Task<Category> Create(Category category);
        Task<Category> Update(Category category);
        Task<Category> Remove(Category category);
    }
}
