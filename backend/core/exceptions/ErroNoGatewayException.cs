using System;

namespace core.usecases.exceptions
{
    public class ErroNoGatewayException : Exception
    {
        public ErroNoGatewayException()
        {
        }

        public ErroNoGatewayException(Exception innerException) : base("", innerException)
        {
        }
    }
}
