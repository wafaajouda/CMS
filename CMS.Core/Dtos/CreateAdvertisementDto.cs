using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dtos
{
    public class CreateAdvertisementDto
    {
        [Required (ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "عنوان الإعلان")]
        public string Title { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "صورة الإعلان")]
        public IFormFile? Image { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Url]
        [Display(Name = "الموقع")]
        public string WebsiteUrl { get; set; }
        [Display(Name = "البداية")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "النهاية")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Display(Name = "السعر")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public decimal Price { get; set; }
        public CreateUserDto Owner { get; set; }
        [Display(Name = "صاحب الإعلان")]
        public string? OwnerId { get; set; }
    }
}
