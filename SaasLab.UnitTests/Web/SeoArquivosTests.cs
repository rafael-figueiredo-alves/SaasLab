using SaasLab.Web.Seo;
using System.Xml.Linq;

namespace SaasLab.UnitTests.Web
{
    public class SeoArquivosTests
    {
        private static readonly SiteOptions Site = new() { UrlBase = "https://exemplo.com.br/" };

        [Fact]
        public void Robots_BloqueiaAreasPrivadasEIndicaOSitemap()
        {
            var robots = SeoArquivos.Robots(Site);

            Assert.Contains("Disallow: /admin", robots);
            Assert.Contains("Disallow: /app", robots);
            Assert.Contains("Sitemap: https://exemplo.com.br/sitemap.xml", robots);
        }

        [Fact]
        public void Sitemap_ListaCadaCaminhoComoUrlAbsoluta()
        {
            var xml = SeoArquivos.Sitemap(Site, ["/", "/precos"]);

            Assert.Contains("<loc>https://exemplo.com.br/</loc>", xml);
            Assert.Contains("<loc>https://exemplo.com.br/precos</loc>", xml);
        }

        [Fact]
        public void Sitemap_EscapaCaracteresEspeciaisDoXml()
        {
            var xml = SeoArquivos.Sitemap(Site, ["/busca?a=1&b=2"]);

            Assert.Contains("a=1&amp;b=2", xml);
        }

        [Fact]
        public void Sitemap_GeraXmlBemFormado()
        {
            var xml = SeoArquivos.Sitemap(Site, ["/"]);

            var documento = XDocument.Parse(xml);   // lança exceção se o XML for inválido

            Assert.Equal("urlset", documento.Root!.Name.LocalName);
        }
    }
}
