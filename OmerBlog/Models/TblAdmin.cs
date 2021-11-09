using System;
using System.Collections.Generic;

namespace OmerBlog.Models
{
    public partial class TblAdmin
    {
        public int AdminId { get; set; }
        public string AdminAd { get; set; }
        public string AdminPassword { get; set; }
        public string AdminEmail { get; set; }
        public string AdminYetki { get; set; }
    }
}
