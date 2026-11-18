using System.Security;
using System.Text;

namespace SaasLab.Web.Seo
{
    /// <summary>Gera o texto do robots.txt e do sitemap.xml. Lógica pura, fácil de testar.</summary>
    public static class SeoArquivos
    {
        /// <summary>Só páginas públicas e indexáveis entram aqui.</summary>
        public static readonly IReadOnlyList<string> PaginasPublicas = ["/"];

        public static string Robots(SiteOptions site) => 
        $"""
            User-agent: *
            Disallow: /admin
            Disallow: /app

            Sitemap: {site.UrlAbsoluta("/sitemap.xml")}
        """;

        public static string Sitemap(SiteOptions site, IEnumerable<string> caminhos)
        {
            var sb = new StringBuilder();
            sb.AppendLine("""<?xml version="1.0" encoding="UTF-8"?>""");
            sb.AppendLine("""<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">""");

            foreach (var caminho in caminhos)
                sb.AppendLine($"  <url><loc>{SecurityElement.Escape(site.UrlAbsoluta(caminho))}</loc></url>");

            sb.AppendLine("</urlset>");
            return sb.ToString();
        }
    }
}
