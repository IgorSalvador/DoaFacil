namespace DoaFacil.Application.DTOs;

public sealed class Result
{
    public bool Succeeded { get; }
    public IReadOnlyCollection<string> Errors { get; }

    private Result(bool succeeded, IEnumerable<string>? errors = null)
    {
        Succeeded = succeeded;
        Errors = errors?.ToArray() ?? [];
    }

    public static Result Success() => new(true);

    public static Result Failure(params string[] errors) => new(false, errors);

    public static Result Failure(IEnumerable<string> errors) => new(false, errors);
}
