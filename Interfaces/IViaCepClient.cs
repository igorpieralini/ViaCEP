using ViaCEP.Api.Models.DTOs;

namespace ViaCEP.Api.Interfaces;

public interface IViaCepClient
{
    Task<ViaCepResponseDto?> BuscarCepAsync(string cep);
}
