using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.ViewModels
{
    public class PostAttachmentViewModel
    {
        public int Id { get; set; }
        public string AttachmentUrl { get; set; }
    }
}
