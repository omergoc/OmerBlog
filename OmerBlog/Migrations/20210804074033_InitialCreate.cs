using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogProje.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_ADMIN",
                columns: table => new
                {
                    AdminId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminAd = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    AdminPassword = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    AdminEmail = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    AdminYetki = table.Column<string>(unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ADMIN", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "TBL_AYARLAR",
                columns: table => new
                {
                    SiteId = table.Column<int>(nullable: false),
                    SiteBaslik = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SiteAciklama = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    SiteAnahtarKelime = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    GoogleAnaliz = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SiteHostAdres = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SitePort = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SiteMail = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SiteSifre = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SiteFacebook = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SiteLinkedin = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SiteTwitter = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SiteGithub = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Siteİnstagram = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_AYARLAR", x => x.SiteId);
                });

            migrationBuilder.CreateTable(
                name: "TBL_DENEYIMLER",
                columns: table => new
                {
                    DeneyimId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeneyimBaslik = table.Column<string>(unicode: false, maxLength: 80, nullable: true),
                    DeneyimAltBaslik = table.Column<string>(unicode: false, maxLength: 80, nullable: true),
                    DeneyimDetay = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    DeneyimBaslangic = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    DeneyimBitis = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_DENEYİMLER", x => x.DeneyimId);
                });

            migrationBuilder.CreateTable(
                name: "TBL_EGITIMLER",
                columns: table => new
                {
                    EgitimId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EgitimBaslik = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    EgitimAltBaslik = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    EgitimDetay = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    EgitimBaslangic = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    EgitimBitis = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_EGİTİMLER", x => x.EgitimId);
                });

            migrationBuilder.CreateTable(
                name: "TBL_HAKKINDA",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Ad = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Soyad = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Eposta = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Telefon = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ulke = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Sehir = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Resim = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Detay = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Adres = table.Column<string>(unicode: false, maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_HAKKINDA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_LISANS",
                columns: table => new
                {
                    LisansId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LisansSahibi = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    LisansSite = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    LisansBaslangic = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    LisansBitis = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    LisansKod = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    LisansNot = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_LİSANS", x => x.LisansId);
                });

            migrationBuilder.CreateTable(
                name: "TBL_PROJELER",
                columns: table => new
                {
                    ProjeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjeBaslik = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ProjeAltBaslik = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ProjeDetay = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    ProjeTarih = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ProjeResim1 = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    ProjeResim2 = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    ProjeResim3 = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_PROJELER", x => x.ProjeId);
                });

            migrationBuilder.CreateTable(
                name: "TBL_YETENEKLER",
                columns: table => new
                {
                    YetenekId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YetenekAd = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_YETENEKLER", x => x.YetenekId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_ADMIN");

            migrationBuilder.DropTable(
                name: "TBL_AYARLAR");

            migrationBuilder.DropTable(
                name: "TBL_DENEYIMLER");

            migrationBuilder.DropTable(
                name: "TBL_EGITIMLER");

            migrationBuilder.DropTable(
                name: "TBL_HAKKINDA");

            migrationBuilder.DropTable(
                name: "TBL_LISANS");

            migrationBuilder.DropTable(
                name: "TBL_PROJELER");

            migrationBuilder.DropTable(
                name: "TBL_YETENEKLER");
        }
    }
}
