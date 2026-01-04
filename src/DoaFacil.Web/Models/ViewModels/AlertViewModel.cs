namespace DoaFacil.Web.Models.ViewModels;

public sealed class AlertViewModel
{
    public string? Title { get; init; }
    public string Message { get; init; } = string.Empty;
    public AlertTone Tone { get; init; } = AlertTone.Info;
}

public enum AlertTone
{
    Info,
    Success,
    Warning,
    Error
}