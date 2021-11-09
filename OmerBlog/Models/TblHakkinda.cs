using System;
using System.Collections.Generic;

namespace OmerBlog.Models
{
    public partial class TblHakkinda
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Eposta { get; set; }
        public string Telefon { get; set; }
        public string Ulke { get; set; }
        public string Sehir { get; set; }
        public string Resim { get; set; }
        public string Detay { get; set; }
        public string Adres { get; set; }
    }
}
