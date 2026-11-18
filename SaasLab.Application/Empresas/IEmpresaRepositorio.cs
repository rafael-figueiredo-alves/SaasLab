using SaasLab.Domain.Empresas;

namespace SaasLab.Application.Empresas
{
    /// <summary>
    /// Porta: o Application diz o que precisa, a Infrastructure (Parte 4) diz como.
    /// Só os métodos que o caso de uso usa, sem repositório genérico.
    /// </summary>
    public interface IEmpresaRepositorio
    {
        Task<bool> ExisteCnpjAsync(Cnpj cnpj, CancellationToken ct);
        Task AdicionarAsync(Empresa empresa, CancellationToken ct);
    }
}
