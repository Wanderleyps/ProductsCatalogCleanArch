﻿using ProductsCatalogCleanArch.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsCatalogCleanArch.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();
        Task<CategoryDTO> GetById(int? id);
        Task Add(CategoryDTO categoryDto);
        Task Update(CategoryDTO categoryDto);
        Task Remove(int? id);
    }
}
