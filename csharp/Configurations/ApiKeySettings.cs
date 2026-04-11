namespace ViaCEP.Api.Configurations;

public class ApiKeySettings
{
    public const string SectionName = "ApiKey";

    /// <summary>Token único de acesso à API. Envie no header: X-Api-Key: {Token}</summary>
    public string Token { get; set; } = string.Empty;
}
