using SaasLab.Application.Empresas;
using SaasLab.Domain.Empresas;
using SaasLab.UnitTests.Fakes;

namespace SaasLab.UnitTests.Application
{
    public class CadastrarEmpresaTests
    {
        private readonly EmpresaRepositorioEmMemoria _repositorio = new();
        private readonly CadastrarEmpresaCasoDeUso _casoDeUso;

        public CadastrarEmpresaTests() => _casoDeUso = new CadastrarEmpresaCasoDeUso(_repositorio);

        [Fact]
        public async Task ExecutarAsync_DadosValidos_RetornaSucessoEGravaEmpresa()
        {
            var resultado = await _casoDeUso.ExecutarAsync(
                new CadastrarEmpresaComando("Padaria Central", "11.444.777/0001-61"));

            Assert.True(resultado.Sucesso);
            var empresa = Assert.Single(_repositorio.Empresas);
            Assert.Equal(resultado.Valor, empresa.Id);
            Assert.Equal("11444777000161", empresa.Cnpj.Valor);
        }

        [Fact]
        public async Task ExecutarAsync_CnpjInvalido_RetornaErroENaoGrava()
        {
            var resultado = await _casoDeUso.ExecutarAsync(
                new CadastrarEmpresaComando("Padaria Central", "11444777000162"));

            Assert.False(resultado.Sucesso);
            Assert.Equal(EmpresaErros.CnpjInvalido, resultado.Erro);
            Assert.Empty(_repositorio.Empresas);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("   ")]
        public async Task ExecutarAsync_NomeVazio_RetornaErroENaoGrava(string? nome)
        {
            var resultado = await _casoDeUso.ExecutarAsync(
                new CadastrarEmpresaComando(nome, "11444777000161"));

            Assert.False(resultado.Sucesso);
            Assert.Equal(EmpresaErros.NomeInvalido, resultado.Erro);
            Assert.Empty(_repositorio.Empresas);
        }

        [Fact]
        public async Task ExecutarAsync_CnpjJaCadastradoEmOutroFormato_RetornaErro()
        {
            await _casoDeUso.ExecutarAsync(new CadastrarEmpresaComando("Padaria Central", "11444777000161"));

            var resultado = await _casoDeUso.ExecutarAsync(
                new CadastrarEmpresaComando("Outra Padaria", "11.444.777/0001-61"));

            Assert.False(resultado.Sucesso);
            Assert.Equal(EmpresaErros.CnpjJaCadastrado, resultado.Erro);
            Assert.Single(_repositorio.Empresas);
        }

        [Fact]
        public async Task ExecutarAsync_NomeMaiorQuePermitido_RetornaErro()
        {
            var resultado = await _casoDeUso.ExecutarAsync(
                new CadastrarEmpresaComando(new string('A', Empresa.NomeMaximo + 1), "11.444.777/0001-61"));

            Assert.False(resultado.Sucesso);
        }
    }
}
