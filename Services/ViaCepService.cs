using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Memory;
using ViaCEP.Api.Configurations;
using ViaCEP.Api.Infrastructure;
using ViaCEP.Api.Interfaces;
using ViaCEP.Api.Models.DTOs;
using ViaCEP.Api.Models.Entities;
using Microsoft.Extensions.Options;

namespace ViaCEP.Api.Services;

public class ViaCepService : IViaCepService
{
    private readonly IViaCepClient _viaCepClient;
    private readonly ILogger<ViaCepService> _logger;
    private readonly IMemoryCache _cache;
    private readonly ViaCepSettings _settings;

    private static readonly Regex _cepRegex = new(@"^\d{8}$", RegexOptions.Compiled);
    private const string CacheKeyPrefix = "cep_";

    public ViaCepService(
        IViaCepClient viaCepClient,
        ILogger<ViaCepService> logger,
        IMemoryCache cache,
        IOptions<ViaCepSettings> settings)
    {
        _viaCepClient = viaCepClient;
        _logger = logger;
        _cache = cache;
        _settings = settings.Value;
    }

    public async Task<EnderecoResponseDto> ConsultarCepAsync(string cep)
    {
        var cepLimpo = cep.Replace("-", "").Trim();

        if (!_cepRegex.IsMatch(cepLimpo))
        {
            _logger.LogWarning("CEP inválido recebido: {Cep}", cep);
            throw new ArgumentException("CEP inválido. Informe somente 8 dígitos numéricos.");
        }

        var cacheKey = CacheKeyPrefix + cepLimpo;
        if (_cache.TryGetValue(cacheKey, out EnderecoResponseDto? cached) && cached is not null)
        {
            _logger.LogInformation("Cache hit para o CEP: {Cep}", cepLimpo);
            return cached;
        }

        ViaCepResponseDto? resposta;

        try
        {
            resposta = await _viaCepClient.BuscarCepAsync(cepLimpo);
        }
        catch (ExternalServiceException)
        {
            throw;
        }

        if (resposta is null || resposta.Erro == true)
        {
            _logger.LogWarning("CEP não encontrado: {Cep}", cepLimpo);
            throw new KeyNotFoundException($"CEP '{cepLimpo}' não encontrado.");
        }

        var endereco = MapearParaEntidade(resposta);
        var resultado = MapearParaDto(endereco);

        _cache.Set(cacheKey, resultado, TimeSpan.FromMinutes(_settings.CacheMinutes));
        _logger.LogInformation("CEP {Cep} armazenado em cache por {Min} minutos.", cepLimpo, _settings.CacheMinutes);

        return resultado;
    }

    private static Endereco MapearParaEntidade(ViaCepResponseDto dto) => new()
    {
        Cep        = dto.Cep        ?? string.Empty,
        Logradouro = dto.Logradouro ?? string.Empty,
        Complemento= dto.Complemento?? string.Empty,
        Bairro     = dto.Bairro     ?? string.Empty,
        Cidade     = dto.Localidade ?? string.Empty,
        Estado     = dto.Uf         ?? string.Empty,
        Ibge       = dto.Ibge       ?? string.Empty,
        Gia        = dto.Gia        ?? string.Empty,
        Ddd        = dto.Ddd        ?? string.Empty,
        Siafi      = dto.Siafi      ?? string.Empty,
    };

    private static EnderecoResponseDto MapearParaDto(Endereco e) => new()
    {
        Cep         = e.Cep,
        Logradouro  = e.Logradouro,
        Complemento = e.Complemento,
        Bairro      = e.Bairro,
        Cidade      = e.Cidade,
        Estado      = e.Estado,
        Ibge        = e.Ibge,
        Gia         = e.Gia,
        Ddd         = e.Ddd,
        Siafi       = e.Siafi,
    };
}
