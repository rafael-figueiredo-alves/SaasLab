using SaasLab.Domain.Empresas;

namespace SaasLab.UnitTests.Domain
{
    public class EmpresaTests
    {
        private static Cnpj CnpjValido()
        {
            Cnpj.TryParse("11444777000161", out var cnpj);
            return cnpj!;
        }

        [Fact]
        public void TryCriar_DadosValidos_RetornaTrueEGuardaNomeSemEspacosNasPontas()
        {
            var ok = Empresa.TryCriar(Guid.NewGuid(), "  Padaria Central  ", CnpjValido(), out var empresa);

            Assert.True(ok);
            Assert.Equal("Padaria Central", empresa!.Nome);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void TryCriar_NomeVazio_RetornaFalse(string? nome)
        {
            var ok = Empresa.TryCriar(Guid.NewGuid(), nome, CnpjValido(), out var empresa);

            Assert.False(ok);
            Assert.Null(empresa);
        }

        [Fact]
        public void TryCriar_NomeMaiorQueOLimite_RetornaFalse()
        {
            var nomeGrande = new string('A', Empresa.NomeMaximo + 1);

            var ok = Empresa.TryCriar(Guid.NewGuid(), nomeGrande, CnpjValido(), out _);

            Assert.False(ok);
        }
    }
}
