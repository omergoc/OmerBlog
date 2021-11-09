using System;
using System.Collections.Generic;

namespace OmerBlog.Models
{
    public partial class TblProjeler
    {
        public int ProjeId { get; set; }
        public string ProjeBaslik { get; set; }
        public string ProjeAltBaslik { get; set; }
        public string ProjeDetay { get; set; }
        public string ProjeTarih { get; set; }
        public string ProjeResim1 { get; set; }
        public string ProjeResim2 { get; set; }
        public string ProjeResim3 { get; set; }
    }
}
