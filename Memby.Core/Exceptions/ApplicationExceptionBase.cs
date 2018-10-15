using System;

namespace Memby.Core.Exceptions
{
    public abstract class ApplicationExceptionBase : Exception
    {
        public ApplicationExceptionBase(string message, string messageCode)
            : base(message)
        {
            MessageCode = messageCode;
        }

        public readonly string MessageCode;
    }
}
