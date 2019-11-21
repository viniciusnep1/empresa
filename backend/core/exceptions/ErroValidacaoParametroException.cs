using System;

namespace core.usecases.exceptions
{
    public class ErroValidacaoParametroException : Exception
    {
        public string NomeEntidade { get; private set; }
        public Guid? EntidadeId { get; private set; }
        public string NomeParametro { get; private set; }
        public string ValorParametro { get; private set; }
        public string Mensagem { get; private set; }

        public ErroValidacaoParametroException(string nomeEntidade, Guid? entidadeId, string nomeParametro,
            object valorParametro, string mensagem = "")
        {
            NomeEntidade = nomeEntidade;
            EntidadeId = entidadeId;
            NomeParametro = nomeParametro;
            ValorParametro = valorParametro?.ToString() ?? "null";
            Mensagem = mensagem;
        }
    }
}
