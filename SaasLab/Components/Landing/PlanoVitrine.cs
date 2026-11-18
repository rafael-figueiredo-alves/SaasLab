namespace SaasLab.Web.Components.Landing
{
    // Modelo só de apresentação. Na Parte 4 passa a ser montado a partir da tabela de planos.
    public sealed record PlanoVitrine(string Nome, decimal PrecoMensal, IReadOnlyList<string> Recursos);
}
