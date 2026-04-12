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
            return BadRequest(new ErrorResponseDto(ex.Message, StatusCodes.Status400BadRequest));
        }
        catch (KeyNotFoundException ex)
        {
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
