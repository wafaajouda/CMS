using CMS.Core.Dtos;
using CMS.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Infrastructure.Services.Advertisements
{
    public interface IAdvertisementService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Query query);
        Task<int> Delete(int id);
        Task<int> Create(CreateAdvertisementDto dto);
        Task<List<UserViewModel>> GetAdvertisementOwner();
    }
}
