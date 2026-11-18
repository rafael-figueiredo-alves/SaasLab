using SaasLab.Application.Empresas;
using SaasLab.Domain.Empresas;

namespace SaasLab.UnitTests.Fakes
{
    /// <summary>
    /// Fake: implementação real e simples da porta, guardando em uma lista.
    /// Sem biblioteca de mock, o código é fácil de ler.
    /// </summary>
    internal sealed class EmpresaRepositorioEmMemoria : IEmpresaRepositorio
    {
        private readonly List<Empresa> _empresas = [];

        public IReadOnlyList<Empresa> Empresas => _empresas;

        public Task<bool> ExisteCnpjAsync(Cnpj cnpj, CancellationToken ct)
            => Task.FromResult(_empresas.Any(e => e.Cnpj == cnpj));

        public Task AdicionarAsync(Empresa empresa, CancellationToken ct)
        {
            _empresas.Add(empresa);
            return Task.CompletedTask;
        }
    }
}
