using System;
using System.Collections.Generic;

namespace OmerBlog.Models
{
    public partial class TblDeneyimler
    {
        public int DeneyimId { get; set; }
        public string DeneyimBaslik { get; set; }
        public string DeneyimAltBaslik { get; set; }
        public string DeneyimDetay { get; set; }
        public string DeneyimBaslangic { get; set; }
        public string DeneyimBitis { get; set; }
    }
}
