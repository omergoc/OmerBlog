using System;
using System.Collections.Generic;

namespace OmerBlog.Models
{
    public partial class TblAyarlar
    {
        public int SiteId { get; set; }
        public string SiteBaslik { get; set; }
        public string SiteAciklama { get; set; }
        public string SiteAnahtarKelime { get; set; }
        public string GoogleAnaliz { get; set; }
        public string SiteHostAdres { get; set; }
        public string SitePort { get; set; }
        public string SiteMail { get; set; }
        public string SiteSifre { get; set; }
        public string SiteFacebook { get; set; }
        public string SiteLinkedin { get; set; }
        public string SiteTwitter { get; set; }
        public string SiteGithub { get; set; }
        public string Siteİnstagram { get; set; }
    }
}
