using Bunit;
using SaasLab.Web.Components.Landing;

namespace SaasLab.UnitTests.Web
{
    public class SecaoPlanosTests : BunitContext
    {
        private static readonly PlanoVitrine[] DoisPlanos =
        [
            new("Básico", 49.90m, ["1 empresa", "3 usuários"]),
            new("Profissional", 129.90m, ["15 usuários"]),
        ];

        [Fact]
        public void Render_DoisPlanos_MostraUmCartaoPorPlanoComNomeEPreco()
        {
            var cut = Render<SecaoPlanos>(p => p.Add(c => c.Planos, DoisPlanos));

            Assert.Equal(2, cut.FindAll("article.plan").Count);
            Assert.Contains("Básico", cut.Markup);
            Assert.Contains("49,90", cut.Markup);   // sem o "R$": o espaço depois dele é não separável
        }

        [Fact]
        public void Render_PlanoComRecursos_MostraTodosOsRecursos()
        {
            var cut = Render<SecaoPlanos>(p => p.Add(c => c.Planos, DoisPlanos));

            Assert.Equal(3, cut.FindAll("article.plan li").Count);
        }

        [Fact]
        public void Render_SemPlanos_MostraMensagemEmBreve()
        {
            var cut = Render<SecaoPlanos>(p => p.Add(c => c.Planos, Array.Empty<PlanoVitrine>()));

            Assert.Empty(cut.FindAll("article.plan"));
            Assert.Contains("Planos em breve", cut.Markup);
        }
    }
}
