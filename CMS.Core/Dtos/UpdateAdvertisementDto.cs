using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dtos
{
    public class UpdateAdvertisementDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "عنوان الإعلان")]
        public string Title { get; set; }
        [Display(Name = "صورة الإعلان")]
        public IFormFile Image { get; set; }
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
        public UpdateUserDto Owner { get; set; }
    }
}
