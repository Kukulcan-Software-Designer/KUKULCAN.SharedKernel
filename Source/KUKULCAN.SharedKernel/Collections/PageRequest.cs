namespace KUKULCAN.SharedKernel.Collections;

/// <summary>
/// Represents a paging request.
/// </summary>
public sealed record PageRequest
{
    /// <summary>
    /// Represents the default page size.
    /// </summary>
    public const int DefaultPageSize = 25;

    /// <summary>
    /// Represents the maximum allowed page size.
    /// </summary>
    public const int MaximumPageSize = 100;

    /// <summary>
    /// Initializes a new instance of the <see cref="PageRequest"/> class.
    /// </summary>
    /// <param name="pageNumber">
    /// Requested page number.
    /// </param>
    /// <param name="pageSize">
    /// Requested page size.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the page number or page size is invalid.
    /// </exception>
    public PageRequest(int pageNumber = 1, int pageSize = DefaultPageSize)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(pageNumber, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, 1);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(pageSize, MaximumPageSize);

        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    /// <summary>
    /// Gets the requested page number.
    /// </summary>
    public int PageNumber { get; }

    /// <summary>
    /// Gets the requested page size.
    /// </summary>
    public int PageSize { get; }
}
