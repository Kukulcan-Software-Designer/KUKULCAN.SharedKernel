using System;
using KUKULCAN.SharedKernel.Results.Internals;

namespace KUKULCAN.SharedKernel.Results;

/// <summary>
/// Represents an immutable error.
/// </summary>
public sealed record Error
{
    /// <summary>
    /// Gets an <see cref="Error"/> that represents the absence of an error.
    /// </summary>
    public static readonly Error None = new(
        CommonErrorCodes.None,
        CommonMessages.None());

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class.
    /// </summary>
    /// <param name="code">
    /// Error code.
    /// </param>
    /// <param name="description">
    /// Error description.
    /// </param>
    /// <exception cref="ArgumentException">
    /// <paramref name="code"/> or <paramref name="description"/> is null, empty or whitespace.
    /// </exception>
    public Error(string code, string description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        Code = code;
        Description = description;
    }

    /// <summary>
    /// Gets the unique error code.
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Gets the error description.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Returns a string representation of the current error.
    /// </summary>
    /// <returns>
    /// A string containing the error code and description.
    /// </returns>
    public override string ToString()
    {
        return $"{Code}: {Description}";
    }
}
