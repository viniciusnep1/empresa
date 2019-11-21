using System;

namespace core.usecases.exceptions
{
    public class ErroNoRepositorioException : Exception
    {
        public ErroNoRepositorioException()
        {
        }

        public ErroNoRepositorioException(Exception innerException) : base("", innerException)
        {
        }
    }
}
