using System;
using System.Collections.Generic;

namespace OmerBlog.Models
{
    public partial class TblEgitimler
    {
        public int EgitimId { get; set; }
        public string EgitimBaslik { get; set; }
        public string EgitimAltBaslik { get; set; }
        public string EgitimDetay { get; set; }
        public string EgitimBaslangic { get; set; }
        public string EgitimBitis { get; set; }
    }
}
