namespace ViaCEP.Api.Models.DTOs;

public class ErrorResponseDto
{
    public string Mensagem { get; set; } = string.Empty;
    public int StatusCode { get; set; }

    public ErrorResponseDto(string mensagem, int statusCode)
    {
        Mensagem = mensagem;
        StatusCode = statusCode;
    }
}
