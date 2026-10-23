using AutoMapper;
using CMS.Core.Constant;
using CMS.Core.Dtos;
using CMS.Core.Exceptions;
using CMS.Core.ViewModels;
using CMS.Data;
using CMS.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Infrastructure.Services.Users
{
    public class UserService
    {
        private readonly CMSDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService; 
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;

        public UserService(CMSDbContext db, IMapper mapper, UserManager<User> userManager, IFileService fileService, IEmailService emailService)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
            _fileService = fileService;
            _emailService = emailService;
        }

        public async Task<string> Create(UserDto dto)
        {
            var emailOrPhoneIsExist = _db.Users.Any(x => !x.IsDelete && (x.Email == dto.Email || x.PhoneNumber == dto.PhoneNumber));
            if(emailOrPhoneIsExist)
            {
                throw new DuplicateEmailOrPhoneException();
            }
            var user = _mapper.Map<User>(dto);
            user.UserName = dto.Email;
            
            if(dto.Image != null)
            {
                user.ImageUrl = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }

            var password = GeneratePassword();
            var result = await _userManager.CreateAsync(user, password);
            if(!result.Succeeded)
            {
                throw new OperationFailedException();
            }

            await _emailService.Send(user.Email, "New Account !", $"Username is : {user.Email} and Password is {password}");
            return user.Id;
        }

        public async Task<List<UserViewModel>> GetAll()
        {
            var users = await _db.Users.Where(x => !x.IsDelete).ToListAsync();
            return _mapper.Map<List<UserViewModel>>(users);
        }

        public async Task<string> Update(UserDto dto)
        {
            var emailOrPhoneIsExist = _db.Users.Any(x => !x.IsDelete && (x.Email == dto.Email || x.PhoneNumber == dto.PhoneNumber) && x.Id != dto.Id);
            if (emailOrPhoneIsExist)
            {
                throw new DuplicateEmailOrPhoneException();
            }
            var user = await _db.Users.FindAsync(dto.Id);
            var updatedUser = _mapper.Map<UserDto, User>(dto, user);
            
            if (dto.Image != null)
            {
                updatedUser.ImageUrl = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }

            _db.Users.Update(updatedUser);
            _db.SaveChangesAsync();
            return user.Id;
        }

        public async Task<string> Delete(string Id)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (user == null)
            {
                throw new EntityNotFoundException();
            }
            user.IsDelete = true;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return user.Id;
        }

        private string GeneratePassword()
        {
            return Guid.NewGuid().ToString().Substring(1, 8);
        }
    }
}
