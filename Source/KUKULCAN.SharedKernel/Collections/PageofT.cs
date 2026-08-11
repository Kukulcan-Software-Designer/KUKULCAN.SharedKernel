namespace KUKULCAN.SharedKernel.Collections;

/// <summary>
/// Represents a single page of a paged collection.
/// </summary>
/// <typeparam name="T">
/// Type of the elements contained in the page.
/// </typeparam>
public sealed record Page<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Page{T}"/> class.
    /// </summary>
    /// <param name="items">
    /// Elements contained in the page.
    /// </param>
    /// <param name="pageNumber">
    /// Current page number.
    /// </param>
    /// <param name="pageSize">
    /// Number of elements requested per page.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="items"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Page number or page size is less than one.
    /// </exception>
    public Page(IReadOnlyList<T> items, int pageNumber, int pageSize)
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentOutOfRangeException.ThrowIfLessThan(pageNumber, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, 1);

        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    /// <summary>
    /// Gets the elements contained in this page.
    /// </summary>
    public IReadOnlyList<T> Items { get; }

    /// <summary>
    /// Gets the current page number.
    /// </summary>
    public int PageNumber { get; }

    /// <summary>
    /// Gets the requested page size.
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Gets the number of elements contained in this page.
    /// </summary>
    public int Count => Items.Count;

    /// <summary>
    /// Gets a value indicating whether the page contains any elements.
    /// </summary>
    public bool HasItems => Count > 0;

    /// <summary>
    /// Gets a value indicating whether the page is empty.
    /// </summary>
    public bool IsEmpty => Count == 0;
}
