using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OmerBlog.Models
{
    public partial class BlogDbContext : DbContext
    {
        public BlogDbContext()
        {
        }

        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAdmin> TblAdmin { get; set; }
        public virtual DbSet<TblAyarlar> TblAyarlar { get; set; }
        public virtual DbSet<TblDeneyimler> TblDeneyimler { get; set; }
        public virtual DbSet<TblEgitimler> TblEgitimler { get; set; }
        public virtual DbSet<TblHakkinda> TblHakkinda { get; set; }
        public virtual DbSet<TblLisans> TblLisans { get; set; }
        public virtual DbSet<TblProjeler> TblProjeler { get; set; }
        public virtual DbSet<TblYetenekler> TblYetenekler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["BloggingDatabase"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAdmin>(entity =>
            {
                entity.HasKey(e => e.AdminId);

                entity.ToTable("TBL_ADMIN");

                entity.Property(e => e.AdminAd)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AdminEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AdminPassword)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AdminYetki)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblAyarlar>(entity =>
            {
                entity.HasKey(e => e.SiteId);

                entity.ToTable("TBL_AYARLAR");

                entity.Property(e => e.SiteId).ValueGeneratedNever();

                entity.Property(e => e.GoogleAnaliz)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SiteAciklama)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SiteAnahtarKelime)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SiteBaslik)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SiteFacebook)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SiteGithub)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SiteHostAdres)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SiteLinkedin)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SiteMail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SitePort)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SiteSifre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SiteTwitter)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Siteİnstagram)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblDeneyimler>(entity =>
            {
                entity.HasKey(e => e.DeneyimId)
                    .HasName("PK_TBL_DENEYİMLER");

                entity.ToTable("TBL_DENEYIMLER");

                entity.Property(e => e.DeneyimAltBaslik)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.DeneyimBaslangic)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeneyimBaslik)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.DeneyimBitis)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeneyimDetay)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblEgitimler>(entity =>
            {
                entity.HasKey(e => e.EgitimId)
                    .HasName("PK_TBL_EGİTİMLER");

                entity.ToTable("TBL_EGITIMLER");

                entity.Property(e => e.EgitimAltBaslik)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EgitimBaslangic)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EgitimBaslik)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EgitimBitis)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EgitimDetay)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblHakkinda>(entity =>
            {
                entity.ToTable("TBL_HAKKINDA");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Ad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Adres)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Detay)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Eposta)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Resim)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sehir)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Soyad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefon)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ulke)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblLisans>(entity =>
            {
                entity.HasKey(e => e.LisansId)
                    .HasName("PK_TBL_LİSANS");

                entity.ToTable("TBL_LISANS");

                entity.Property(e => e.LisansBaslangic)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LisansBitis)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LisansKod)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LisansNot)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LisansSahibi)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LisansSite)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblProjeler>(entity =>
            {
                entity.HasKey(e => e.ProjeId);

                entity.ToTable("TBL_PROJELER");

                entity.Property(e => e.ProjeAltBaslik)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProjeBaslik)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProjeDetay)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ProjeResim1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProjeResim2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProjeResim3)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProjeTarih)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblYetenekler>(entity =>
            {
                entity.HasKey(e => e.YetenekId);

                entity.ToTable("TBL_YETENEKLER");

                entity.Property(e => e.YetenekAd)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
