using Microsoft.AspNetCore.Html;

namespace DoaFacil.Web.Models.ViewModels;

public sealed class CardViewModel
{
    public string? Title { get; init; }
    public string? Subtitle { get; init; }

    public IHtmlContent? Body { get; init; }
}