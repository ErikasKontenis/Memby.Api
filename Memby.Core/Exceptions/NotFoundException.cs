namespace Memby.Core.Exceptions
{
    public class NotFoundException : ApplicationExceptionBase
    {
        public NotFoundException(string message, string messageCode)
            : base(message, messageCode)
        { }
    }
}
