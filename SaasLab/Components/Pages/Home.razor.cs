using Microsoft.AspNetCore.Components;
using SaasLab.Web.Components.Landing;

namespace SaasLab.Web.Components.Pages
{
    public class HomeBase : ComponentBase
    {
        [Inject] protected IWebHostEnvironment Ambiente { get; set; } = default!;
        protected bool _cacheHabilitado;
        protected TimeSpan _expiraCache => TimeSpan.FromMinutes(10);

        // Em desenvolvimento o cache fica desligado, para você ver cada alteração na hora.
        protected override void OnInitialized() => _cacheHabilitado = !Ambiente.IsDevelopment();

        // Fixo por enquanto; na Parte 4 vem da tabela de planos.
        protected static readonly PlanoVitrine[] _planos =
        [
            new("Básico", 49.90m, ["1 empresa", "3 usuários", "Relatórios padrão"]),
            new("Profissional", 129.90m, ["1 empresa", "15 usuários", "Relatórios personalizados"]),
            new("Empresarial", 349.90m, ["Várias empresas", "Usuários ilimitados", "Suporte prioritário"]),
        ];
    }
}
