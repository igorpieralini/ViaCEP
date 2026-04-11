namespace ViaCEP.Api.Configurations;

public class ApiKeySettings
{
    public const string SectionName = "ApiKey";
    public string Token { get; set; } = string.Empty;
}
