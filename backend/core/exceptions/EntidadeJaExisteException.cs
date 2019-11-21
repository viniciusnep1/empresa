using System;

namespace core.usecases.exceptions
{
    public class EntidadeJaExisteException : Exception
    {
        public string NomeEntidade { get; private set; }
        public Guid EntidadeId { get; private set; }

        public EntidadeJaExisteException(string nomeEntidade, Guid entidadeId)
        {
            EntidadeId = entidadeId;
            NomeEntidade = nomeEntidade;
        }
    }
}
