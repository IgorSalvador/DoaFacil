namespace DoaFacil.Web.Models.ViewModels;


public sealed class InputViewModel
{
    public string? Label { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Type { get; init; } = "text";
    public string? Placeholder { get; init; }
    public string? Value { get; init; }

    public string? Error { get; init; }
    public string? Hint { get; init; }
}