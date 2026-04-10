using Microsoft.Extensions.Options;
using ViaCEP.Api.Configurations;
using ViaCEP.Api.Models.DTOs;
using System.Text.Json;

namespace ViaCEP.Api.Middleware;

public class ApiKeyMiddleware
{
    private const string HeaderName = "X-Api-Key";
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiKeyMiddleware> _logger;

    public ApiKeyMiddleware(RequestDelegate next, ILogger<ApiKeyMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, IOptions<ApiKeySettings> settings)
    {
        // Swagger e OpenAPI passam livremente
        var path = context.Request.Path.Value ?? string.Empty;
        if (path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase))
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue(HeaderName, out var receivedKey)
            || receivedKey != settings.Value.Token)
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
