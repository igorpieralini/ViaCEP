using System.Net.Http.Json;
using System.Text.Json;
using ViaCEP.Api.Interfaces;
using ViaCEP.Api.Models.DTOs;

namespace ViaCEP.Api.Infrastructure;

public class ViaCepClient : IViaCepClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ViaCepClient> _logger;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public ViaCepClient(HttpClient httpClient, ILogger<ViaCepClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<ViaCepResponseDto?> BuscarCepAsync(string cep)
    {
        var url = $"{cep}/json/";

        _logger.LogInformation("Consultando ViaCEP para o CEP: {Cep}", cep);

        HttpResponseMessage response;

        try
        {
            response = await _httpClient.GetAsync(url);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Erro de comunicação ao consultar ViaCEP para o CEP: {Cep}", cep);
            throw new ExternalServiceException("Falha ao comunicar com o serviço ViaCEP.", ex);
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex, "Timeout ao consultar ViaCEP para o CEP: {Cep}", cep);
            throw new ExternalServiceException("Timeout ao consultar o serviço ViaCEP.", ex);
        }

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("ViaCEP retornou status {StatusCode} para o CEP: {Cep}",
                response.StatusCode, cep);
            throw new ExternalServiceException($"ViaCEP retornou status inesperado: {(int)response.StatusCode}");
        }

        var resultado = await response.Content.ReadFromJsonAsync<ViaCepResponseDto>(_jsonOptions);

        _logger.LogInformation("Resposta recebida da ViaCEP para o CEP: {Cep}", cep);

        return resultado;
    }
}
