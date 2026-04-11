using Microsoft.Extensions.Options;
using ViaCEP.Api.Configurations;
using ViaCEP.Api.Models.DTOs;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ViaCEP.Api.Middleware;

public class ApiKeyMiddleware
{
    private const string HeaderName = "X-Api-Key";
    private static readonly string[] _openPaths = new[] { "/swagger", "/health" };
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiKeyMiddleware> _logger;

    public ApiKeyMiddleware(RequestDelegate next, ILogger<ApiKeyMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, IOptions<ApiKeySettings> settings)
    {
        var path = context.Request.Path.Value ?? string.Empty;
        if (_openPaths.Any(p => path.StartsWith(p, StringComparison.OrdinalIgnoreCase)))
        {
            await _next(context);
            return;
        }

        var isValid = context.Request.Headers.TryGetValue(HeaderName, out var receivedKey)
            && CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(receivedKey.ToString()),
                Encoding.UTF8.GetBytes(settings.Value.Token));

        if (!isValid)
        {
            _logger.LogWarning("Acesso negado: token inválido ou ausente. Path: {Path}", path);
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            var erro = new ErrorResponseDto("Token de acesso inválido ou ausente.", StatusCodes.Status401Unauthorized);
            await context.Response.WriteAsync(JsonSerializer.Serialize(erro));
            return;
        }

        await _next(context);
    }
}
