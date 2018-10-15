namespace Memby.Core.Exceptions
{
    public class ValidationException : ApplicationExceptionBase
    {
        public ValidationException(string message, string messageCode)
            : base(message, messageCode)
        { }
    }
}
