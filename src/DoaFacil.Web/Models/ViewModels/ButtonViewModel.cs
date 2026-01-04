namespace DoaFacil.Web.Models.ViewModels;

public sealed class ButtonViewModel
{
    public string Text { get; init; } = "Button";
    public string? Href { get; init; }
    public string Variant { get; init; } = "primary";
    public string Size { get; init; } = "md";
}