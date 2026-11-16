using CMS.Core.Dtos;
using CMS.Core.Enums;
using CMS.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Infrastructure.Services.Posts
{
    public interface IPostService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Query query);
        Task<int> Create(CreatePostDto dto);
        Task<UpdatePostDto> Get(int id);
        Task<int> Delete(int id);
        Task<List<ContentChangeLogViewModel>> GetLog(int id);
        Task<int> UpdateStatus(int id, ContentStatus status);
    }
}
