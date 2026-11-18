using SaasLab.Application.Comum;
using SaasLab.Domain.Empresas;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaasLab.Application.Empresas
{
    public sealed record CadastrarEmpresaComando(string? Nome, string? Cnpj);

    public sealed class CadastrarEmpresaCasoDeUso(IEmpresaRepositorio repositorio)
    {
        public async Task<Resultado<Guid>> ExecutarAsync(
            CadastrarEmpresaComando comando, CancellationToken ct = default)
        {
            if (!Cnpj.TryParse(comando.Cnpj, out var cnpj))
                return Resultado<Guid>.Falha(EmpresaErros.CnpjInvalido);

            // Guid.CreateVersion7: Guid ordenado por tempo, que costuma dar índices melhores no banco.
            if (!Empresa.TryCriar(Guid.CreateVersion7(), comando.Nome, cnpj, out var empresa))
                return Resultado<Guid>.Falha(EmpresaErros.NomeInvalido);

            if (await repositorio.ExisteCnpjAsync(cnpj, ct))
                return Resultado<Guid>.Falha(EmpresaErros.CnpjJaCadastrado);

            await repositorio.AdicionarAsync(empresa, ct);

            return Resultado<Guid>.Ok(empresa.Id);
        }
    }
}
