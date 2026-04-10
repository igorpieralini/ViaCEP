namespace ViaCEP.Api.Models.DTOs;

/// <summary>DTO retornado ao cliente da API.</summary>
public class EnderecoResponseDto
{
    /// <example>01310-100</example>
    public string Cep { get; set; } = string.Empty;

    /// <example>Avenida Paulista</example>
    public string Logradouro { get; set; } = string.Empty;

    /// <example>de 610 a 610 - lado par</example>
    public string Complemento { get; set; } = string.Empty;

    /// <example>Bela Vista</example>
    public string Bairro { get; set; } = string.Empty;

    /// <example>São Paulo</example>
    public string Cidade { get; set; } = string.Empty;

    /// <example>SP</example>
    public string Estado { get; set; } = string.Empty;

    /// <example>3550308</example>
    public string Ibge { get; set; } = string.Empty;

    /// <example>1004</example>
    public string Gia { get; set; } = string.Empty;

    /// <example>11</example>
    public string Ddd { get; set; } = string.Empty;

    /// <example>7107</example>
    public string Siafi { get; set; } = string.Empty;
}
