using CMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.ViewModels
{
    public class ContentChangeLogViewModel
    {
        public DateTime ChangeAt { get; set; }
        public ContentStatus Old { get; set; }
        public ContentStatus New { get; set; }
    }
}
