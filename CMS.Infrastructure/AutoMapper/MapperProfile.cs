using AutoMapper;
using CMS.Core.Dtos;
using CMS.Core.ViewModels;
using CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Infrastructure.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserViewModel>().ForMember(x => x.UserType, x => x.MapFrom(x => x.UserType.ToString()));
            CreateMap<CreateUserDto, User>().ForMember(x => x.ImageUrl, x => x.Ignore());
            CreateMap<UpdateUserDto, User>().ForMember(x => x.ImageUrl, x => x.Ignore());
            CreateMap<User, UpdateUserDto>().ForMember(x => x.Image, x => x.Ignore());

            CreateMap<Category, CategoryViewModel>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Category, UpdateCategoryDto>();

            CreateMap<Advertisement, AdvertisementViewModel>().ForMember(x => x.StartDate, x => x.MapFrom(x => x.StartDate.ToString("yyyy:MM:dd"))).ForMember(x => x.EndDate, x => x.MapFrom(x => x.EndDate.ToString("yyyy:MM:dd")));
            CreateMap<CreateAdvertisementDto, Advertisement>().ForMember(x => x.ImageUrl, x => x.Ignore()).ForMember(x => x.Owner, x => x.Ignore());
            CreateMap<UpdateAdvertisementDto, Advertisement>().ForMember(x => x.ImageUrl, x => x.Ignore());
            CreateMap<Advertisement, UpdateAdvertisementDto>().ForMember(x => x.Image, x => x.Ignore()); ;
        }
    }
}
