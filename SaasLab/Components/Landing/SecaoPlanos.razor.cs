using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace SaasLab.Web.Components.Landing
{
    public class SecaoPlanosBase : ComponentBase
    {
        protected static readonly CultureInfo Cultura = new("pt-BR");

        // Componente "burro": só recebe dados e desenha. Por isso é fácil de testar com bUnit.
        [Parameter, EditorRequired]
        public IReadOnlyList<PlanoVitrine> Planos { get; set; } = [];
    }
}
