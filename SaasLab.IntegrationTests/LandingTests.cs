using System.Net;

namespace SaasLab.IntegrationTests
{
    public sealed class LandingTests(SaasLabFactory fabrica) : IClassFixture<SaasLabFactory>
    {
        private readonly HttpClient _cliente = fabrica.CreateClient();

        [Fact]
        public async Task Get_Home_RetornaHtmlComIdiomaPlanosECanonical()
        {
            var resposta = await _cliente.GetAsync("/");

            // O Blazor codifica acentos como &#xE1;. O navegador decodifica, então o teste também.
            var html = WebUtility.HtmlDecode(await resposta.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.OK, resposta.StatusCode);
            Assert.Contains("lang=\"pt-BR\"", html);
            Assert.Contains("Básico", html);
            Assert.Contains("rel=\"canonical\"", html);
            Assert.Contains("href=\"https://exemplo.com.br/\"", html);
        }

        [Fact]
        public async Task Get_Robots_RetornaTextoComSitemap()
        {
            var resposta = await _cliente.GetAsync("/robots.txt");
            var texto = await resposta.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, resposta.StatusCode);
            Assert.Equal("text/plain", resposta.Content.Headers.ContentType?.MediaType);
            Assert.Contains("Sitemap: https://exemplo.com.br/sitemap.xml", texto);
        }

        [Fact]
        public async Task Get_Sitemap_RetornaXmlComAHome()
        {
            var resposta = await _cliente.GetAsync("/sitemap.xml");
            var xml = await resposta.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, resposta.StatusCode);
            Assert.Equal("application/xml", resposta.Content.Headers.ContentType?.MediaType);
            Assert.Contains("<loc>https://exemplo.com.br/</loc>", xml);
        }
    }
}
