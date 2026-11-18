namespace SaasLab.Web.Seo
{
    /// <summary>Options pattern: configuração tipada, lida da seção "Site" do appsettings.</summary>
    public sealed class SiteOptions
    {
        public const string Secao = "Site";

        public string UrlBase { get; set; } = "";

        public string UrlAbsoluta(string caminho) => UrlBase.TrimEnd('/') + caminho;
    }
}
