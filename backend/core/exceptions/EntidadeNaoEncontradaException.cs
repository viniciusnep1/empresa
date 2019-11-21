using System;

namespace core.usecases.exceptions
{
    public class EntidadeNaoEncontradaException : Exception
    {
        public string NomeEntidade { get; private set; }
        public string Propriedade { get; private set; }
        public string Valor { get; private set; }

        public EntidadeNaoEncontradaException(string nomeEntidade, string propriedade, string valor)
        {
            Propriedade = propriedade;
            NomeEntidade = nomeEntidade;
            Valor = valor;
        }
    }
}
