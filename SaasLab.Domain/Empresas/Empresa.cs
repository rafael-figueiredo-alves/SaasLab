using System.Diagnostics.CodeAnalysis;

namespace SaasLab.Domain.Empresas
{
    /// <summary>
    /// Entidade: tem identidade (Id). Duas empresas com o mesmo nome continuam sendo empresas diferentes.
    /// </summary>
    public sealed class Empresa
    {
        public const int NomeMaximo = 200;

        private Empresa(Guid id, string nome, Cnpj cnpj)
        {
            Id = id;
            Nome = nome;
            Cnpj = cnpj;
        }

        public Guid Id { get; }
        public string Nome { get; }
        public Cnpj Cnpj { get; }

        public static bool TryCriar(Guid id, string? nome, Cnpj cnpj, [NotNullWhen(true)] out Empresa? empresa)
        {
            empresa = null;

            var nomeLimpo = nome?.Trim();
            if (string.IsNullOrEmpty(nomeLimpo) || nomeLimpo.Length > NomeMaximo)
                return false;

            empresa = new Empresa(id, nomeLimpo, cnpj);
            return true;
        }
    }
}
