using SaasLab.Web.Seo;

namespace SaasLab.UnitTests.Web
{
    public class SiteOptionsTests
    {
        [Theory]
        [InlineData("https://exemplo.com.br", "/", "https://exemplo.com.br/")]
        [InlineData("https://exemplo.com.br/", "/", "https://exemplo.com.br/")]
        [InlineData("https://exemplo.com.br/", "/precos", "https://exemplo.com.br/precos")]
        public void UrlAbsoluta_JuntaBaseECaminhoSemBarraDuplicada(string urlBase, string caminho, string esperado)
        {
            var site = new SiteOptions { UrlBase = urlBase };

            Assert.Equal(esperado, site.UrlAbsoluta(caminho));
        }
    }
}
