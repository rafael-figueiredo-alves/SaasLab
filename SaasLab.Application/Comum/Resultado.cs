namespace SaasLab.Application.Comum
{
    public sealed record Erro(string Codigo, string Mensagem);

    /// <summary>
    /// Resultado de um caso de uso: ou tem valor (sucesso) ou tem erro. Sem exceção para fluxo normal.
    /// </summary>
    public sealed class Resultado<T>
    {
        private readonly T? _valor;
        private readonly Erro? _erro;

        private Resultado(T? valor, Erro? erro)
        {
            _valor = valor;
            _erro = erro;
        }

        public bool Sucesso => _erro is null;

        public T Valor => Sucesso
            ? _valor!
            : throw new InvalidOperationException("Resultado com falha não tem valor.");

        public Erro Erro => !Sucesso
            ? _erro!
            : throw new InvalidOperationException("Resultado de sucesso não tem erro.");

        public static Resultado<T> Ok(T valor) => new(valor, null);
        public static Resultado<T> Falha(Erro erro) => new(default, erro);
    }
}
