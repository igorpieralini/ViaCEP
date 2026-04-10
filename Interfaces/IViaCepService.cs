using ViaCEP.Api.Models.DTOs;

namespace ViaCEP.Api.Interfaces;

public interface IViaCepService
{
    Task<EnderecoResponseDto> ConsultarCepAsync(string cep);
}
