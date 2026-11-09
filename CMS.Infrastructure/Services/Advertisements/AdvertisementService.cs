using AutoMapper;
using CMS.Core.Constant;
using CMS.Core.Dtos;
using CMS.Core.Enums;
using CMS.Core.Exceptions;
using CMS.Core.ViewModels;
using CMS.Data;
using CMS.Data.Models;
using CMS.Infrastructure.Services.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Infrastructure.Services.Advertisements
{
    public class AdvertisementService : IAdvertisementService
    {
        public readonly CMSDbContext _db;
        public readonly IMapper _mapper;
        public readonly IUserService _userService;
        public readonly IFileService _fileService;
        public AdvertisementService(CMSDbContext db, IMapper mapper, IUserService userService, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _userService = userService;
            _fileService = fileService;
        }

        public async Task<List<UserViewModel>> GetAdvertisementOwner()
        {
            var users = await _db.Users.Where(x => !x.IsDelete && x.UserType == UserType.AdvertisementOwner).ToListAsync();
            return _mapper.Map<List<UserViewModel>>(users);
        }

        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.Advertisements.Include(x => x.Owner).Where(x => !x.IsDelete).AsQueryable();

            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var advertisements = _mapper.Map<List<AdvertisementViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);

            var result = new ResponseDto
            {
                data = advertisements,
                meta = new Meta
                {
                    page = pagination.Page,
                    perpage = pagination.PerPage,
                    pages = pages,
                    total = dataCount,
                }
            };


            return result;
        }

        public async Task<int> Delete(int id)
        {
            var advertisement = await _db.Advertisements.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (advertisement == null)
            {
                throw new EntityNotFoundException();
            }
            advertisement.IsDelete = true;
            _db.Advertisements.Update(advertisement);
            await _db.SaveChangesAsync();
            return advertisement.Id;
        }

        public async Task<int> Create(CreateAdvertisementDto dto)
        {
            if(dto.StartDate >= dto.EndDate)
            {
                throw new InvalidDateException();
            }
            var advertisement = _mapper.Map<Advertisement>(dto);
            if (dto.Image != null)
            {
                advertisement.ImageUrl = await _fileService.SaveFile(dto.Image, "Images");
            }

            if (string.IsNullOrWhiteSpace(dto.OwnerId))
            {
                advertisement.OwnerId = dto.OwnerId;
            }

            await _db.Advertisements.AddAsync(advertisement);
            await _db.SaveChangesAsync();

            if(advertisement.OwnerId == null)
            {
                var userId = await _userService.Create(dto.Owner);
                advertisement.OwnerId = userId;
                _db.Advertisements.Update(advertisement);
                await _db.SaveChangesAsync();

            }

            return advertisement.Id;
        }
    }
}
