using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OmerBlog.Models;

namespace OmerBlog.Controllers
{
    public class HomeController : Controller
    {
        private BlogDbContext _context;
        public HomeController(BlogDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
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
            var egitim = _context.TblEgitimler.ToList();
            ViewBag.egitimler = egitim;
            var deneyimler = _context.TblDeneyimler.ToList();
            ViewBag.deneyimler = deneyimler;
            var yetenekler = _context.TblYetenekler.ToList();
            ViewBag.yetenekler = yetenekler;
            var projeler = _context.TblProjeler.ToList();
            ViewBag.projeler = projeler;
            var siteayar = _context.TblAyarlar.FirstOrDefault();
            ViewBag.SiteBaslik = siteayar.SiteBaslik;
            ViewBag.SiteFacebook = siteayar.SiteFacebook;
            ViewBag.Siteİnstagram = siteayar.Siteİnstagram;
            ViewBag.SiteTwitter = siteayar.SiteTwitter;
            ViewBag.SiteGithub = siteayar.SiteGithub;
            ViewBag.SiteLinkedin = siteayar.SiteLinkedin;

            return View();
        }
        public IActionResult NotFound(int code)
        {
            ViewBag.code = code;
            return View();
        }
    }
}
