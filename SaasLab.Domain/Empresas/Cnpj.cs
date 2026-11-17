using System.Diagnostics.CodeAnalysis;

namespace SaasLab.Domain.Empresas
{
    public sealed record Cnpj
    {
        public string Valor => throw new NotImplementedException();
        public string Formatado => throw new NotImplementedException();

        public static bool TryParse(string? entrada, [NotNullWhen(true)] out Cnpj? cnpj)
            => throw new NotImplementedException();
    }
}
