using SaasLab.Application.Comum;

namespace SaasLab.Application.Empresas
{
    public static class EmpresaErros
    {
        public static readonly Erro CnpjInvalido =
            new("empresa.cnpj_invalido", "O CNPJ informado é inválido.");

        public static readonly Erro NomeInvalido =
            new("empresa.nome_invalido", "Informe o nome da empresa (até 200 caracteres).");

        public static readonly Erro CnpjJaCadastrado =
            new("empresa.cnpj_ja_cadastrado", "Já existe uma empresa com este CNPJ.");
    }
}
