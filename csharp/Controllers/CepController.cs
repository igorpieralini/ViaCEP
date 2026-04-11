using Microsoft.AspNetCore.Mvc;
using ViaCEP.Api.Infrastructure;
using ViaCEP.Api.Interfaces;
using ViaCEP.Api.Models.DTOs;

namespace ViaCEP.Api.Controllers;

[ApiController]
[Route("api/cep")]
[Produces("application/json")]
public class CepController : ControllerBase
{
    private readonly IViaCepService _viaCepService;
    private readonly ILogger<CepController> _logger;

    public CepController(IViaCepService viaCepService, ILogger<CepController> logger)
    {
        _viaCepService = viaCepService;
        _logger = logger;
    }

    /// <summary>Consulta informações de endereço para o CEP informado.</summary>
    /// <param name="cep">CEP com 8 dígitos numéricos (com ou sem hífen).</param>
    /// <returns>Dados do endereço correspondente ao CEP.</returns>
    /// <remarks>
    /// Exemplos de requisição:
    ///
    ///     GET /api/cep/01310100
    ///     GET /api/cep/01310-100
    ///
    /// Exemplo de resposta (200 OK):
    ///
    ///     {
    ///       "cep": "01310-100",
    ///       "logradouro": "Avenida Paulista",
    ///       "complemento": "de 610 a 610 - lado par",
    ///       "bairro": "Bela Vista",
    ///       "cidade": "São Paulo",
    ///       "estado": "SP",
    ///       "ibge": "3550308",
    ///       "gia": "1004",
    ///       "ddd": "11",
    ///       "siafi": "7107"
    ///     }
    /// </remarks>
    /// <response code="200">Endereço encontrado com sucesso.</response>
    /// <response code="400">CEP informado possui formato inválido.</response>
    /// <response code="404">CEP não encontrado na base da ViaCEP.</response>
    /// <response code="500">Erro interno ou falha na comunicação com o serviço ViaCEP.</response>
    [HttpGet("{cep}")]
    [ProducesResponseType(typeof(EnderecoResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCep([FromRoute] string cep, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Requisição recebida para o CEP: {Cep}", cep);

        try
        {
            var endereco = await _viaCepService.ConsultarCepAsync(cep, cancellationToken);
            return Ok(endereco);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Formato inválido de CEP: {Cep}. Detalhe: {Mensagem}", cep, ex.Message);
            return BadRequest(new ErrorResponseDto(ex.Message, StatusCodes.Status400BadRequest));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning("CEP não encontrado: {Cep}. Detalhe: {Mensagem}", cep, ex.Message);
            return NotFound(new ErrorResponseDto(ex.Message, StatusCodes.Status404NotFound));
        }
        catch (ExternalServiceException ex)
        {
            _logger.LogError(ex, "Falha na integração com ViaCEP para o CEP: {Cep}", cep);
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ErrorResponseDto("Erro ao consultar o serviço externo ViaCEP.", StatusCodes.Status500InternalServerError));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao processar CEP: {Cep}", cep);
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ErrorResponseDto("Ocorreu um erro interno inesperado.", StatusCodes.Status500InternalServerError));
        }
    }
}
