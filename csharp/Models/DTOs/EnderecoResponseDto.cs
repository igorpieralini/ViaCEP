namespace ViaCEP.Api.Models.DTOs;

public class EnderecoResponseDto
{
    public string Cep { get; set; } = string.Empty;
    public string Logradouro { get; set; } = string.Empty;
    public string Complemento { get; set; } = string.Empty;
    public string Bairro { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string Ibge { get; set; } = string.Empty;
    public string Gia { get; set; } = string.Empty;
    public string Ddd { get; set; } = string.Empty;
    public string Siafi { get; set; } = string.Empty;
}
