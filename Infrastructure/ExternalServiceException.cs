namespace ViaCEP.Api.Infrastructure;

public class ExternalServiceException : Exception
{
    public ExternalServiceException(string message) : base(message) { }

    public ExternalServiceException(string message, Exception innerException)
        : base(message, innerException) { }
}
