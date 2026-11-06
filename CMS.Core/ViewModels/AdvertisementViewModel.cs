using CMS.Core.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.ViewModels
{
    public class AdvertisementViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ImageUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal? Price { get; set; }
        public UserViewModel Owner { get; set; }
    }
}
