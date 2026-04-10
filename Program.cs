using ViaCEP.Api.Configurations;
using ViaCEP.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ── Logging ──────────────────────────────────────────────────────────────────
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// ── Controllers ───────────────────────────────────────────────────────────────
builder.Services.AddControllers();

// ── Swagger / OpenAPI ─────────────────────────────────────────────────────────
builder.Services.AddSwaggerDocumentation();

// ── Token único (API Key) ────────────────────────────────────────────────────
builder.Services.AddApiKeyAuthentication(builder.Configuration);

// ── Domínio e Infraestrutura ──────────────────────────────────────────────────
builder.Services.AddViaCepServices(builder.Configuration);

// ─────────────────────────────────────────────────────────────────────────────
var app = builder.Build();

// ── Pipeline de requisição ────────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ViaCEP API v1");
        c.RoutePrefix = string.Empty; // Swagger na raiz
    });
}

app.UseMiddleware<ApiKeyMiddleware>();
app.MapControllers();

app.Run();
