namespace DoaFacil.Application.Services;

public interface ICurrentUser
{
    /// <summary>
    /// Gets a value indicating whether the current user is authenticated.
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// Gets the unique identifier of the current user, if available.
    /// </summary>
    string? UserId { get; }

    /// <summary>
    /// Gets the email address of the current user, if available.
    /// </summary>
    string? Email { get; }

}
