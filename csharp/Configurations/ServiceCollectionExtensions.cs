using ViaCEP.Api.Configurations;
using ViaCEP.Api.Infrastructure;
using ViaCEP.Api.Interfaces;
using ViaCEP.Api.Services;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;

namespace ViaCEP.Api.Configurations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddViaCepServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var settings = configuration
            .GetSection(ViaCepSettings.SectionName)
            .Get<ViaCepSettings>() ?? new ViaCepSettings();

        services.Configure<ViaCepSettings>(
            configuration.GetSection(ViaCepSettings.SectionName));

        services.AddMemoryCache();

        services
            .AddHttpClient<IViaCepClient, ViaCepClient>(client =>
            {
                client.BaseAddress = new Uri(settings.BaseUrl);
                client.Timeout = TimeSpan.FromSeconds(settings.TimeoutSeconds);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddPolicyHandler(HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt))));

        services.AddScoped<IViaCepService, ViaCepService>();

        return services;
    }

    public static IServiceCollection AddApiKeyAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ApiKeySettings>(configuration.GetSection(ApiKeySettings.SectionName));
        return services;
    }

    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title       = "ViaCEP API",
                Version     = "v1",
                Description = "API para consulta de enderecos via servico ViaCEP. Autenticacao via header X-Api-Key.",
                Contact     = new OpenApiContact { Name = "Equipe de Desenvolvimento" }
            });

            var apiKeyScheme = new OpenApiSecurityScheme
            {
                Name        = "X-Api-Key",
                Description = "Informe o token unico de acesso.",
                In          = ParameterLocation.Header,
                Type        = SecuritySchemeType.ApiKey,
                Reference   = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "X-Api-Key"
                }
            };
            options.AddSecurityDefinition("X-Api-Key", apiKeyScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { apiKeyScheme, Array.Empty<string>() }
            });

            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
                options.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}
