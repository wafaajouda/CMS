using CMS.Core.Dtos;
using CMS.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Infrastructure.Services.Categories
{
    public interface ICategoryService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Query query);
        Task<int> Create(CreateCategoryDto dto);
        Task<int> Update(UpdateCategoryDto dto);
        Task<int> Delete(int Id);
        Task<UpdateCategoryDto> Get(int Id);
        Task<List<CategoryViewModel>> GetCategoryList();
    }
}
