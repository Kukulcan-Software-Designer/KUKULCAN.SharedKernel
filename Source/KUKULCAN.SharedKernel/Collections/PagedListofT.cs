namespace KUKULCAN.SharedKernel.Collections;

/// <summary>
/// Represents a page of results together with the total number of available items.
/// </summary>
/// <typeparam name="T">
/// Type of the elements contained in the page.
/// </typeparam>
public sealed record PagedList<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
    /// </summary>
    /// <param name="page">
    /// Page containing the current subset of items.
    /// </param>
    /// <param name="totalCount">
    /// Total number of items available across all pages.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="page"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="totalCount"/> is negative.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="totalCount"/> is smaller than the number of items in <paramref name="page"/>.
    /// </exception>
    public PagedList(Page<T> page, int totalCount)
    {
        ArgumentNullException.ThrowIfNull(page);
        ArgumentOutOfRangeException.ThrowIfNegative(totalCount);

        if (totalCount < page.Count)
        {
            throw new ArgumentException("Total count cannot be smaller than the number of items contained in the page.",
                nameof(totalCount));
        }

        Page = page;
        TotalCount = totalCount;
    }

    /// <summary>
    /// Gets the current page.
    /// </summary>
    public Page<T> Page { get; }

    /// <summary>
    /// Gets the total number of items available across all pages.
    /// </summary>
    public int TotalCount { get; }

    /// <summary>
    /// Gets the current page number.
    /// </summary>
    public int PageNumber => Page.PageNumber;

    /// <summary>
    /// Gets the requested page size.
    /// </summary>
    public int PageSize => Page.PageSize;

    /// <summary>
    /// Gets the items contained in the current page.
    /// </summary>
    public IReadOnlyList<T> Items => Page.Items;

    /// <summary>
    /// Gets the number of items contained in the current page.
    /// </summary>
    public int Count => Page.Count;

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int TotalPages =>
        TotalCount == 0
            ? 0
            : (int)Math.Ceiling((double)TotalCount / PageSize);

    /// <summary>
    /// Gets a value indicating whether a previous page exists.
    /// </summary>
    public bool HasPreviousPage =>
        PageNumber > 1;

    /// <summary>
    /// Gets a value indicating whether a next page exists.
    /// </summary>
    public bool HasNextPage =>
        PageNumber < TotalPages;

    /// <summary>
    /// Gets a value indicating whether this is the first page.
    /// </summary>
    public bool IsFirstPage =>
        PageNumber == 1;

    /// <summary>
    /// Gets a value indicating whether this is the last page.
    /// </summary>
    public bool IsLastPage =>
        !HasNextPage;
}
