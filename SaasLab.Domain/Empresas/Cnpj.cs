using System.Diagnostics.CodeAnalysis;

namespace SaasLab.Domain.Empresas
{
    /// <summary>
    /// Value Object: imutável, igualdade por valor e sempre válido.
    /// A única forma de criar é por TryParse, então um Cnpj inválido não existe.
    /// </summary>
    public sealed record Cnpj
    {
        private const int Tamanho = 14;
        private static readonly int[] PesosPrimeiroDv = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        private static readonly int[] PesosSegundoDv = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

        /// <summary>14 caracteres em maiúsculas, sem máscara. É o que vai para o banco.</summary>
        public string Valor { get; }

        private Cnpj(string valor) => Valor = valor;

        public string Formatado => $"{Valor[..2]}.{Valor[2..5]}.{Valor[5..8]}/{Valor[8..12]}-{Valor[12..]}";

        public static bool TryParse(string? entrada, [NotNullWhen(true)] out Cnpj? cnpj)
        {
            cnpj = null;

            var limpo = RemoverMascara(entrada);

            if (limpo.Length != Tamanho) return false;
            if (!limpo.All(char.IsAsciiLetterOrDigit)) return false;

            var valor = limpo.ToUpperInvariant();

            if (valor.Distinct().Count() == 1) return false;       // 00000000000000, etc.
            if (!char.IsAsciiDigit(valor[12]) || !char.IsAsciiDigit(valor[13])) return false;

            if (CalcularDv(valor.AsSpan(0, 12), PesosPrimeiroDv) != valor[12] - '0') return false;
            if (CalcularDv(valor.AsSpan(0, 13), PesosSegundoDv) != valor[13] - '0') return false;

            cnpj = new Cnpj(valor);
            return true;
        }

        private static string RemoverMascara(string? entrada)
        {
            if (string.IsNullOrWhiteSpace(entrada)) return string.Empty;

            var semMascara = entrada.Where(c => c is not ('.' or '/' or '-') && !char.IsWhiteSpace(c));
            return new string(semMascara.ToArray());
        }

        // Valor de cada caractere = código ASCII - 48 ('0'..'9' => 0..9, 'A' => 17, 'B' => 18...).
        private static int CalcularDv(ReadOnlySpan<char> baseCnpj, int[] pesos)
        {
            var soma = 0;
            for (var i = 0; i < baseCnpj.Length; i++)
                soma += (baseCnpj[i] - '0') * pesos[i];

            var resto = soma % 11;
            return resto < 2 ? 0 : 11 - resto;
        }
    }
}
