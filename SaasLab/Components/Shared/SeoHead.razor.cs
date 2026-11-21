using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using SaasLab.Web.Seo;

namespace SaasLab.Web.Components.Shared
{
    public class SeoHeadBase : ComponentBase
    {
        [Inject] protected IOptions<SiteOptions> Site { get; set; } = default!;
        [Parameter, EditorRequired] public string Titulo { get; set; } = "";
        [Parameter, EditorRequired] public string Descricao { get; set; } = "";
        [Parameter] public string Caminho { get; set; } = "/";

        // As páginas de login e as áreas privadas passam Indexavel="false".
        [Parameter] public bool Indexavel { get; set; } = true;

        protected string UrlCanonica => Site.Value.UrlAbsoluta(Caminho);
    }
}
