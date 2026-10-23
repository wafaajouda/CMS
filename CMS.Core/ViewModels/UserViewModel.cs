using CMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CMS.Core.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BOD { get; set; }
        public string? ImageUrl { get; set; }
        public UserType UserType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
