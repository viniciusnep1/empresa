using System;

namespace core.web.exceptions
{
    public class RequestInvalidoException : Exception
    {
        public RequestInvalidoException(string mensagem) : base(mensagem)
        { }

        public RequestInvalidoException(string mensagem, Exception exception) : base(mensagem, exception)
        { }
    }
}
