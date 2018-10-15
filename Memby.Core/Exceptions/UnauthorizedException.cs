namespace Memby.Core.Exceptions
{
    public class UnauthorizedException : ApplicationExceptionBase
    {
        public UnauthorizedException(string message, string messageCode)
            : base(message, messageCode)
        { }
    }
}
