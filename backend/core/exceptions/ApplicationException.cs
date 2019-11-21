using System;

namespace core.usecases.exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException()
        {
        }

        public ApplicationException(Exception innerException) : base("", innerException)
        {
        }
    }
}
