namespace ViaCEP.Api.Configurations;

public class ViaCepSettings
{
    public const string SectionName = "ViaCep";

    /// <summary>URL base da API ViaCEP.</summary>
    public string BaseUrl { get; set; } = "https://viacep.com.br/ws/";

    /// <summary>Timeout em segundos para chamadas HTTP.</summary>
    public int TimeoutSeconds { get; set; } = 10;

    /// <summary>Tempo em minutos para manter CEPs em cache (0 = cache desativado).</summary>
    public int CacheMinutes { get; set; } = 60;
}
