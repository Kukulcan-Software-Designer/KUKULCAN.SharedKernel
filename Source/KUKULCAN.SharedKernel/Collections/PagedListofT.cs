using System;

namespace KUKULCAN.SharedKernel.Collections;

public sealed record PagedList<T>
{
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

    public Page<T> Page { get; }

    public int TotalCount { get; }

    public int PageNumber => Page.PageNumber;

    public int PageSize => Page.PageSize;

    public IReadOnlyList<T> Items => Page.Items;

    public int Count => Page.Count;

    public int TotalPages =>
        TotalCount == 0
            ? 0
            : (int)Math.Ceiling((double)TotalCount / PageSize);

    public bool HasPreviousPage =>
        PageNumber > 1;

    public bool HasNextPage =>
        PageNumber < TotalPages;

    public bool IsFirstPage =>
        PageNumber == 1;

    public bool IsLastPage =>
        !HasNextPage;
}
