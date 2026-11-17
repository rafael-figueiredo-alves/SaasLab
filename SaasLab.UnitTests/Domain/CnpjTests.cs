using SaasLab.Domain.Empresas;

namespace SaasLab.UnitTests.Domain
{
    public class CnpjTests
    {
        [Theory]
        [InlineData("11444777000161")]        // numérico, sem máscara
        [InlineData("11.444.777/0001-61")]    // numérico, com máscara
        [InlineData("12ABC34501DE35")]        // alfanumérico
        [InlineData("12.ABC.345/01DE-35")]    // alfanumérico, com máscara
        [InlineData("12abc34501de35")]        // minúsculas
        public void TryParse_CnpjValido_RetornaTrue(string entrada)
        {
            var ok = Cnpj.TryParse(entrada, out var cnpj);

            Assert.True(ok);
            Assert.NotNull(cnpj);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("123")]                   // curto demais
        [InlineData("114447770001611")]       // longo demais
        [InlineData("11444777000162")]        // dígito verificador errado
        [InlineData("12ABC34501DE36")]        // dígito verificador errado (alfanumérico)
        [InlineData("12ABC34501DEAB")]        // DV com letras: DV é sempre numérico
        [InlineData("11444777000!61")]        // caractere inválido
        [InlineData("00000000000000")]        // todos iguais: passa no cálculo, mas é inválido
        [InlineData("11111111111111")]
        public void TryParse_CnpjInvalido_RetornaFalse(string? entrada)
        {
            var ok = Cnpj.TryParse(entrada, out var cnpj);

            Assert.False(ok);
            Assert.Null(cnpj);
        }

        [Fact]
        public void Valor_EntradaComMascaraEMinusculas_GuardaSoCaracteresEmMaiusculas()
        {
            Cnpj.TryParse("12.abc.345/01de-35", out var cnpj);

            Assert.Equal("12ABC34501DE35", cnpj!.Valor);
        }

        [Fact]
        public void Formatado_RetornaMascaraPadrao()
        {
            Cnpj.TryParse("12ABC34501DE35", out var cnpj);

            Assert.Equal("12.ABC.345/01DE-35", cnpj!.Formatado);
        }

        [Fact]
        public void Equals_MesmoCnpjEmFormatosDiferentes_SaoIguais()
        {
            Cnpj.TryParse("11444777000161", out var a);
            Cnpj.TryParse("11.444.777/0001-61", out var b);

            Assert.Equal(a, b);
        }
    }
}
