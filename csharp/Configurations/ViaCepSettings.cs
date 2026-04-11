namespace ViaCEP.Api.Configurations;

public class ViaCepSettings
{
    public const string SectionName = "ViaCep";
    public string BaseUrl { get; set; } = "https://viacep.com.br/ws/";
    public int TimeoutSeconds { get; set; } = 10;
    public int CacheMinutes { get; set; } = 60;
}
