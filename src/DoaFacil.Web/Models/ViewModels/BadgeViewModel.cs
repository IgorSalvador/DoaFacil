namespace DoaFacil.Web.Models.ViewModels;

public sealed class BadgeViewModel
{
    public string Text { get; init; } = "Badge";
    public BadgeTone Tone { get; init; } = BadgeTone.Gray;
}

public enum BadgeTone
{
    Gray,
    Green,
    Amber,
    Red,
    Blue
}