using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dtos
{
    public class CreateCategoryDto
    {
        [Required]
        [Display(Name= "اسم التصنيف")]
        public string Name { get; set; }
    }
}
