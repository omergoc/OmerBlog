using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;
using OmerBlog.Models;

namespace OmerBlog.Controllers
{
    public class AdminController : Controller
    {
        public string hash = "omer";
        public string Encrypt(string sifre)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(sifre);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }
        public string Decrypt(string SifrelenmisDeger)
        {
            byte[] data = Convert.FromBase64String(SifrelenmisDeger);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return UTF8Encoding.UTF8.GetString(results);
                }
            }
        }
        private BlogDbContext _context;
        public string GetSession(string key)
        {
            return HttpContext.Session.GetString(key);
        }
        public void SetSession(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }
        public AdminController(BlogDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {

                return View();
            }
        }
        public IActionResult Intrusion()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Login(TblAdmin T)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                string sifre = Encrypt(T.AdminPassword);
                var adminbul = _context.TblAdmin.Where(x => x.AdminEmail == T.AdminEmail && x.AdminPassword == sifre).FirstOrDefault();

                if (adminbul == null)
                {
                    ViewBag.islem = "Kullanıcı Adı Yada Parola Hatalı";
                    return View();
                }
                else
                {
                    SetSession("admin", adminbul.AdminEmail);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public IActionResult UserSettings()
        {

            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var adminsec = _context.TblAdmin.Where(x => x.AdminEmail == admin).FirstOrDefault();
                ViewBag.email = adminsec.AdminEmail;
                ViewBag.ad = adminsec.AdminAd;
                ViewBag.sifre = adminsec.AdminPassword;
                
                return View();
            }
        }
        [HttpPost]
        public IActionResult UserSettings(TblAdmin T)
        {

            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var adminsec = _context.TblAdmin.Where(x => x.AdminEmail == admin).FirstOrDefault();
                string sifre = Encrypt(T.AdminPassword);
                var id = adminsec.AdminId;
                var adminbul = _context.TblAdmin.Find(id);
                adminbul.AdminAd = T.AdminAd;
                adminbul.AdminPassword = sifre;
                _context.TblAdmin.Update(adminbul);
                _context.SaveChanges();
                ViewBag.email = adminsec.AdminEmail;
                ViewBag.ad = adminsec.AdminAd;
                ViewBag.sifre = adminsec.AdminPassword;

                return View();
            }
        }
        public IActionResult Ability()
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {

                var yetenek = _context.TblYetenekler.ToList();
                return View(yetenek);
            }
        }
        [HttpPost]
        public IActionResult Ability(TblYetenekler T)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                _context.TblYetenekler.Add(T);
                _context.SaveChanges();
                var yetenek = _context.TblYetenekler.ToList();
                return View(yetenek);
            }
        }
        public IActionResult AbilityDelete(int id)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var yeteneksil = _context.TblYetenekler.Find(id);
                _context.TblYetenekler.Remove(yeteneksil);
                _context.SaveChanges();
                return RedirectToAction("Ability");
            }

        }
        public IActionResult About()
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var hakkinda = _context.TblHakkinda.FirstOrDefault();
                ViewBag.ad = hakkinda.Ad;
                ViewBag.soyad = hakkinda.Soyad;
                ViewBag.eposta = hakkinda.Eposta;
                ViewBag.telefon = hakkinda.Telefon;
                ViewBag.ulke = hakkinda.Ulke;
                ViewBag.sehir = hakkinda.Sehir;
                ViewBag.adres = hakkinda.Adres;
                ViewBag.resim = hakkinda.Resim;
                ViewBag.detay = hakkinda.Detay;

                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> About(TblHakkinda T, IFormFile file)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var id = 1;
                var hakkinda = _context.TblHakkinda.Where(x => x.Id == id).FirstOrDefault();
                hakkinda.Ad = T.Ad;
                hakkinda.Soyad = T.Soyad;
                hakkinda.Eposta = T.Eposta;
                hakkinda.Telefon = T.Telefon;
                hakkinda.Ulke = T.Ulke;
                hakkinda.Sehir = T.Sehir;
                hakkinda.Detay = T.Detay;

                if(file != null)
                {
                    var exentiont = Path.GetExtension(file.FileName);
                    var randomName = string.Format($"{Guid.NewGuid()}{exentiont}");
                    hakkinda.Resim = randomName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);


                    using (var stream = new FileStream(path,FileMode.Create))
                    {

                       await file.CopyToAsync(stream); 
                    }
                }
                _context.TblHakkinda.Update(hakkinda);
                _context.SaveChanges();




                var hakkindacek = _context.TblHakkinda.FirstOrDefault();

                ViewBag.ad = hakkindacek.Ad;
                ViewBag.soyad = hakkindacek.Soyad;
                ViewBag.eposta = hakkindacek.Eposta;
                ViewBag.telefon = hakkindacek.Telefon;
                ViewBag.ulke = hakkindacek.Ulke;
                ViewBag.sehir = hakkindacek.Sehir;
                ViewBag.adres = hakkindacek.Adres;
                ViewBag.resim = hakkindacek.Resim;
                ViewBag.detay = hakkindacek.Detay;
                ViewBag.islem = "İşlem Başarılı";
                return View();
            }
        }
        public IActionResult Education()
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Education(TblEgitimler T)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                _context.TblEgitimler.Add(T);
                _context.SaveChanges();
                return RedirectToAction("EducationList");
            }
        }
        public IActionResult EducationList()
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var egitimlist = _context.TblEgitimler.ToList();
                return View(egitimlist);
            }
        }
        public IActionResult EducationUpdate(int id)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var egitim = _context.TblEgitimler.Where(x => x.EgitimId == id).FirstOrDefault();
                ViewBag.baslik = egitim.EgitimBaslik;
                ViewBag.altbaslik = egitim.EgitimAltBaslik;
                ViewBag.detay = egitim.EgitimDetay;
                ViewBag.baslangic = egitim.EgitimBaslangic;
                ViewBag.bitis = egitim.EgitimBitis;
                return View();
            }
        }
        [HttpPost]
        public IActionResult EducationUpdate(TblEgitimler T, int id)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var egitimupdate = _context.TblEgitimler.Where(x => x.EgitimId == id).FirstOrDefault();
                egitimupdate.EgitimBaslik = T.EgitimBaslik;
                egitimupdate.EgitimAltBaslik = T.EgitimAltBaslik;
                egitimupdate.EgitimDetay = T.EgitimDetay;
                egitimupdate.EgitimBaslangic = T.EgitimBaslangic;
                egitimupdate.EgitimBitis = T.EgitimBitis;
                _context.TblEgitimler.Update(egitimupdate);
                _context.SaveChanges();
                return RedirectToAction("EducationList");
            }
        }
        public IActionResult EducationDelete(int id)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var egitimsil = _context.TblEgitimler.Where(x => x.EgitimId == id).FirstOrDefault();
                _context.TblEgitimler.Remove(egitimsil);
                _context.SaveChanges();
                return RedirectToAction("EducationList");
            }

        }
        public IActionResult Experience()
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Experience(TblDeneyimler T)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                _context.TblDeneyimler.Add(T);
                _context.SaveChanges();
                return RedirectToAction("ExperienceList");
            }
        }

        public IActionResult ExperienceList()
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var deneyimlist = _context.TblDeneyimler.ToList();
                return View(deneyimlist);
            }
        }
        public IActionResult ExperienceUpdate(int id)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var deneyim = _context.TblDeneyimler.Where(x => x.DeneyimId == id).FirstOrDefault();
                ViewBag.baslik = deneyim.DeneyimBaslik;
                ViewBag.altbaslik = deneyim.DeneyimAltBaslik;
                ViewBag.detay = deneyim.DeneyimDetay;
                ViewBag.baslangic = deneyim.DeneyimBaslangic;
                ViewBag.bitis = deneyim.DeneyimBitis;
                ViewBag.id = deneyim.DeneyimId;
                return View();
            }

        }
        [HttpPost]
        public IActionResult ExperienceUpdate(TblDeneyimler T,int id)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var deneyimupdate = _context.TblDeneyimler.Where(x => x.DeneyimId == id).FirstOrDefault();
                deneyimupdate.DeneyimBaslik = T.DeneyimBaslik;
                deneyimupdate.DeneyimAltBaslik = T.DeneyimAltBaslik;
                deneyimupdate.DeneyimDetay = T.DeneyimDetay;
                deneyimupdate.DeneyimBaslangic = T.DeneyimBaslangic;
                deneyimupdate.DeneyimBitis = T.DeneyimBitis;
                _context.TblDeneyimler.Update(deneyimupdate);
                _context.SaveChanges();
                return RedirectToAction("ExperienceList");
            }
        }
        public IActionResult ExperienceDelete(int id)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var deneyimsil = _context.TblDeneyimler.Where(x => x.DeneyimId == id).FirstOrDefault();
                _context.TblDeneyimler.Remove(deneyimsil);
                _context.SaveChanges();
                return RedirectToAction("ExperienceList");
            }
        }
        public IActionResult License()
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult License(TblLisans T)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                _context.TblLisans.Add(T);
                _context.SaveChanges();

                return RedirectToAction("LicenseList");
            }

        }
        public IActionResult LicenseList()
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var lisanlist = _context.TblLisans.ToList();
                return View(lisanlist);
            }

        }
        public IActionResult LicenseUpdate(int id)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var lisans = _context.TblLisans.Find(id);

                ViewBag.sahibi = lisans.LisansSahibi;
                ViewBag.site = lisans.LisansSite;
                ViewBag.baslangic = lisans.LisansBaslangic;
                ViewBag.bitis = lisans.LisansBitis;
                ViewBag.kod = lisans.LisansKod;
                ViewBag.not = lisans.LisansNot;
                return View();
            }

        }
        [HttpPost]
        public IActionResult LicenseUpdate(int id, TblLisans T)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var lisanssec = _context.TblLisans.Find(id);
                lisanssec.LisansSahibi = T.LisansSahibi;
                lisanssec.LisansSite = T.LisansSite;
                lisanssec.LisansBaslangic = T.LisansBaslangic;
                lisanssec.LisansBitis = T.LisansBitis;
                lisanssec.LisansKod = T.LisansKod;
                lisanssec.LisansNot = T.LisansNot;
                _context.TblLisans.Update(lisanssec);
                _context.SaveChanges();
                return RedirectToAction("LicenseList");
            }
        }
        public IActionResult LicenseDelete(int id)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var lisansdelete = _context.TblLisans.Find(id);
                _context.TblLisans.Remove(lisansdelete);
                _context.SaveChanges();
                return RedirectToAction("LicenseList");
            }
        }
        public IActionResult Project()
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Project(TblProjeler T,IFormFile file, IFormFile file2, IFormFile file3)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {

                if (file != null)
                {
                    var exentiont = Path.GetExtension(file.FileName);
                    var randomName = string.Format($"{Guid.NewGuid()}{exentiont}");
                    T.ProjeResim1 = randomName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);


                    using (var stream = new FileStream(path, FileMode.Create))
                    {

                        await file.CopyToAsync(stream);
                    }
                }
                if (file2 != null)
                {
                    var exentiont = Path.GetExtension(file2.FileName);
                    var randomName = string.Format($"{Guid.NewGuid()}{exentiont}");
                    T.ProjeResim2 = randomName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);


                    using (var stream = new FileStream(path, FileMode.Create))
                    {

                        await file2.CopyToAsync(stream);
                    }
                }
                if (file3 != null)
                {
                    var exentiont = Path.GetExtension(file3.FileName);
                    var randomName = string.Format($"{Guid.NewGuid()}{exentiont}");
                    T.ProjeResim3 = randomName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);


                    using (var stream = new FileStream(path, FileMode.Create))
                    {

                        await file3.CopyToAsync(stream);
                    }
                }
                _context.TblProjeler.Add(T);
                _context.SaveChanges();
                return RedirectToAction("ProjectList");
            }

        }
        public IActionResult ProjectList()
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var proje = _context.TblProjeler.ToList();

                return View(proje);
            }
        }
        public IActionResult ProjectUpdate(int id)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var proje = _context.TblProjeler.Find(id);

                ViewBag.baslik = proje.ProjeBaslik;
                ViewBag.altbaslik = proje.ProjeAltBaslik;
                ViewBag.detay = proje.ProjeDetay;
                ViewBag.tarih = proje.ProjeTarih;
                ViewBag.resim = proje.ProjeResim1;
                ViewBag.resim2 = proje.ProjeResim2;
                ViewBag.resim3 = proje.ProjeResim3;
                return View();
            }

        }
        [HttpPost]
        public async Task<IActionResult> ProjectUpdate(TblProjeler T, int id, IFormFile file, IFormFile file2, IFormFile file3)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var proje = _context.TblProjeler.Where(x => x.ProjeId == id).FirstOrDefault();
                proje.ProjeBaslik = T.ProjeBaslik;
                proje.ProjeAltBaslik = T.ProjeAltBaslik;
                proje.ProjeDetay = T.ProjeDetay;
                proje.ProjeTarih = T.ProjeTarih;

                if (file != null)
                {
                    var exentiont = Path.GetExtension(file.FileName);
                    var randomName = string.Format($"{Guid.NewGuid()}{exentiont}");
                    proje.ProjeResim1 = randomName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);


                    using (var stream = new FileStream(path, FileMode.Create))
                    {

                        await file.CopyToAsync(stream);
                    }
                }
                if (file2 != null)
                {
                    var exentiont2 = Path.GetExtension(file2.FileName);
                    var randomName2 = string.Format($"{Guid.NewGuid()}{exentiont2}");
                    proje.ProjeResim2 = randomName2;
                    var path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName2);


                    using (var stream2 = new FileStream(path2, FileMode.Create))
                    {

                        await file2.CopyToAsync(stream2);
                    }
                }
                if (file3 != null)
                {
                    var exentiont3 = Path.GetExtension(file3.FileName);
                    var randomName3 = string.Format($"{Guid.NewGuid()}{exentiont3}");
                    proje.ProjeResim3 = randomName3;
                    var path3 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName3);


                    using (var stream3 = new FileStream(path3, FileMode.Create))
                    {

                        await file3.CopyToAsync(stream3);
                    }
                }
                _context.TblProjeler.Update(proje);
                _context.SaveChanges();
                return RedirectToAction("ProjectList");
            }

        }
        public IActionResult ProjectDelete(int id)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var projesec = _context.TblProjeler.Find(id);
                _context.TblProjeler.Remove(projesec);
                _context.SaveChanges();
                return RedirectToAction("ProjectList");
            }
        }
        public IActionResult GeneralSettings()
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var ayarlar = _context.TblAyarlar.Find(1);
                ViewBag.baslik = ayarlar.SiteBaslik;
                ViewBag.aciklama = ayarlar.SiteAciklama;
                ViewBag.kelime = ayarlar.SiteAnahtarKelime;
                ViewBag.analiz = ayarlar.GoogleAnaliz;
                ViewBag.host = ayarlar.SiteHostAdres;
                ViewBag.mail = ayarlar.SiteMail;
                ViewBag.port = ayarlar.SitePort;
                ViewBag.sifre = ayarlar.SiteSifre;

                return View();
            }

        }
        [HttpPost]
        public IActionResult GeneralSettings(TblAyarlar T)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var id = 1;
                var siteayar = _context.TblAyarlar.Find(id);
                siteayar.SiteBaslik = T.SiteBaslik;
                siteayar.SiteAciklama = T.SiteAciklama;
                siteayar.SiteAnahtarKelime = T.SiteAnahtarKelime;
                siteayar.GoogleAnaliz = T.GoogleAnaliz;
                siteayar.SiteHostAdres = T.SiteHostAdres;
                siteayar.SiteMail = T.SiteMail;
                siteayar.SitePort = T.SitePort;
                siteayar.SiteSifre = T.SiteSifre;
                _context.TblAyarlar.Update(siteayar);
                _context.SaveChanges();

                var ayarlar = _context.TblAyarlar.FirstOrDefault();

                ViewBag.baslik = ayarlar.SiteBaslik;
                ViewBag.aciklama = ayarlar.SiteAciklama;
                ViewBag.kelime = ayarlar.SiteAnahtarKelime;
                ViewBag.analiz = ayarlar.GoogleAnaliz;
                ViewBag.host = ayarlar.SiteHostAdres;
                ViewBag.mail = ayarlar.SiteMail;
                ViewBag.port = ayarlar.SitePort;
                ViewBag.sifre = ayarlar.SiteSifre;
                ViewBag.bilgi = 1;
                return View();
            }
        }
        public IActionResult SocialMedia()
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var ayarlar = _context.TblAyarlar.Find(1);
                ViewBag.facebook = ayarlar.SiteFacebook;
                ViewBag.linkedin = ayarlar.SiteLinkedin;
                ViewBag.github = ayarlar.SiteGithub;
                ViewBag.instagram = ayarlar.Siteİnstagram;
                ViewBag.twitter = ayarlar.SiteTwitter;

                return View();
            }
        }
        [HttpPost]
        public IActionResult SocialMedia(TblAyarlar T)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var id = 1;
                var siteayar = _context.TblAyarlar.Find(id);
                siteayar.SiteFacebook = T.SiteFacebook;
                siteayar.SiteTwitter = T.SiteTwitter;
                siteayar.SiteGithub = T.SiteGithub;
                siteayar.Siteİnstagram = T.Siteİnstagram;
                siteayar.SiteLinkedin = T.SiteLinkedin;

                _context.TblAyarlar.Update(siteayar);
                _context.SaveChanges();

                var ayarlar = _context.TblAyarlar.Find(1);
                ViewBag.facebook = ayarlar.SiteFacebook;
                ViewBag.linkedin = ayarlar.SiteLinkedin;
                ViewBag.github = ayarlar.SiteGithub;
                ViewBag.instagram = ayarlar.Siteİnstagram;
                ViewBag.twitter = ayarlar.SiteTwitter;
                ViewBag.bilgi = 1;

                return View();
            }
        }
        public  IActionResult Users()
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var adminsec = _context.TblAdmin.Where(x => x.AdminEmail == admin).FirstOrDefault();
                if (adminsec.AdminYetki == "1")
                {
                    var users = _context.TblAdmin.ToList();
                    return View(users);
                }
                else
                {
                    return RedirectToAction("YetkiYok");
                }
            }
        }
        [HttpPost]
        public IActionResult Users(TblAdmin T)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var adminsec = _context.TblAdmin.Where(x => x.AdminEmail == admin).FirstOrDefault();
                if(adminsec.AdminYetki =="1")
                {

                var usermail = _context.TblAdmin.Where(x => x.AdminEmail == T.AdminEmail).FirstOrDefault();
                if (usermail == null)
                {

                    string sifre = T.AdminPassword;
                    T.AdminPassword =  Encrypt(sifre);
                    _context.TblAdmin.Add(T);
                    _context.SaveChanges();
                    var users = _context.TblAdmin.ToList();
                    return View(users);
                }
                else
                {
                    ViewBag.islem = 1;
                    var users = _context.TblAdmin.ToList();
                    return View(users);
                }
                }
                else
                {
                    return RedirectToAction("YetkiYok");
                }
            }
        }
        public IActionResult UsersUpdate(int id)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var adminsec = _context.TblAdmin.Where(x => x.AdminEmail == admin).FirstOrDefault();
                if (adminsec.AdminYetki == "1")
                {
                    var usersec = _context.TblAdmin.Find(id);
                    ViewBag.ad = usersec.AdminAd;
                    ViewBag.sifre = usersec.AdminPassword;
                    ViewBag.email = usersec.AdminEmail;
                    return View();
                }
                else
                {
                    return RedirectToAction("YetkiYok");
                }
            }
        }
        [HttpPost]
        public IActionResult UsersUpdate(int id, TblAdmin T)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var adminsec = _context.TblAdmin.Where(x => x.AdminEmail == admin).FirstOrDefault();
                if (adminsec.AdminYetki == "1")
                {
                    string sifre = Encrypt(T.AdminPassword);
                    var usersec = _context.TblAdmin.Find(id);
                    usersec.AdminAd = T.AdminAd;
                    usersec.AdminPassword = sifre;
                    usersec.AdminEmail = T.AdminEmail;
                    _context.TblAdmin.Update(usersec);
                    _context.SaveChanges();

                    return RedirectToAction("Users");
                }
                else
                {
                    return RedirectToAction("YetkiYok");
                }
            }
        }
        public IActionResult UsersDelete(int id)
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                var adminsec = _context.TblAdmin.Where(x => x.AdminEmail == admin).FirstOrDefault();
                if (adminsec.AdminYetki == "1")
                {
                    var user = _context.TblAdmin.Find(id);
                    _context.TblAdmin.Remove(user);
                    _context.SaveChanges();
                    return RedirectToAction("Users");
                }
                else
                {
                    return RedirectToAction("YetkiYok");
                }
            }
        }
        public IActionResult YetkiYok() 
        {
            var admin = GetSession("admin");

            if (admin == null)
            {
                return RedirectToAction("Intrusion");
            }
            else
            {
                return View();
            }
        }
    }
}
