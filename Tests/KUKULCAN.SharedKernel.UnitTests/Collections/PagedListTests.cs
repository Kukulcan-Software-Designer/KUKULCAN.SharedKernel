using KUKULCAN.SharedKernel.Collections;

namespace KUKULCAN.SharedKernel.UnitTests.Collections;

/// <summary>
/// Contains unit tests for <see cref="PagedList{T}"/>.
/// </summary>
[TestFixture]
public sealed class PagedListTests
{
    #region Construction

    /// <summary>
    /// Verifies that valid constructor arguments are assigned.
    /// </summary>
    [Test]
    public void Constructor_WithValidValues_ShouldAssignProperties()
    {
        var page = new Page<string>(
            ["One", "Two"],
            2,
            10);

        var pagedList = new PagedList<string>(
            page,
            25);

        Assert.Multiple(() =>
        {
            Assert.That(pagedList.Page, Is.SameAs(page));
            Assert.That(pagedList.TotalCount, Is.EqualTo(25));
        });
    }

    /// <summary>
    /// Verifies that a null page is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithNullPage_ShouldThrowArgumentNullException()
    {
        Assert.That(
            () => new PagedList<string>(null!, 0),
            Throws.ArgumentNullException);
    }

    /// <summary>
    /// Verifies that a negative total count is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithNegativeTotalCount_ShouldThrowArgumentOutOfRangeException()
    {
        var page = new Page<string>(
            ["One"],
            1,
            10);

        Assert.That(
            () => new PagedList<string>(page, -1),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    /// <summary>
    /// Verifies that a total count smaller than the page count is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithTotalCountSmallerThanPageCount_ShouldThrowArgumentException()
    {
        var page = new Page<string>(
            ["One", "Two", "Three"],
            1,
            10);

        Assert.That(
            () => new PagedList<string>(page, 2),
            Throws.TypeOf<ArgumentException>());
    }

    #endregion

    #region Delegated properties

    /// <summary>
    /// Verifies that PageNumber is delegated to the underlying page.
    /// </summary>
    [Test]
    public void PageNumber_ShouldReturnPageNumber()
    {
        var page = new Page<string>(
            ["One"],
            3,
            10);

        var pagedList = new PagedList<string>(
            page,
            25);

        Assert.That(
            pagedList.PageNumber,
            Is.EqualTo(3));
    }

    /// <summary>
    /// Verifies that PageSize is delegated to the underlying page.
    /// </summary>
    [Test]
    public void PageSize_ShouldReturnPageSize()
    {
        var page = new Page<string>(
            ["One"],
            1,
            10);

        var pagedList = new PagedList<string>(
            page,
            25);

        Assert.That(
            pagedList.PageSize,
            Is.EqualTo(10));
    }

    /// <summary>
    /// Verifies that Items exposes the items from the underlying page.
    /// </summary>
    [Test]
    public void Items_ShouldExposePageItems()
    {
        var items = new List<string>
        {
            "One",
            "Two"
        };

        var page = new Page<string>(
            items,
            1,
            10);

        var pagedList = new PagedList<string>(
            page,
            25);

        Assert.That(
            pagedList.Items,
            Is.SameAs(page.Items));
    }

    /// <summary>
    /// Verifies that Count returns the number of items on the current page.
    /// </summary>
    [Test]
    public void Count_ShouldReturnPageCount()
    {
        var page = new Page<string>(
            ["One", "Two"],
            1,
            10);

        var pagedList = new PagedList<string>(
            page,
            25);

        Assert.That(
            pagedList.Count,
            Is.EqualTo(2));
    }

    #endregion

    #region Pagination

    /// <summary>
    /// Verifies that TotalPages is zero when there are no elements.
    /// </summary>
    [Test]
    public void TotalPages_WithZeroTotalCount_ShouldReturnZero()
    {
        var page = new Page<string>(
            [],
            1,
            10);

        var pagedList = new PagedList<string>(
            page,
            0);

        Assert.That(
            pagedList.TotalPages,
            Is.EqualTo(0));
    }

    /// <summary>
    /// Verifies that TotalPages rounds up when the total count is not
    /// evenly divisible by the page size.
    /// </summary>
    [Test]
    public void TotalPages_WithPartialLastPage_ShouldRoundUp()
    {
        var page = new Page<string>(
            ["One"],
            1,
            10);

        var pagedList = new PagedList<string>(
            page,
            21);

        Assert.That(
            pagedList.TotalPages,
            Is.EqualTo(3));
    }

    /// <summary>
    /// Verifies that TotalPages is exact when the total count is evenly
    /// divisible by the page size.
    /// </summary>
    [Test]
    public void TotalPages_WithExactPages_ShouldReturnExactNumber()
    {
        var page = new Page<string>(
            ["One"],
            1,
            10);

        var pagedList = new PagedList<string>(
            page,
            20);

        Assert.That(
            pagedList.TotalPages,
            Is.EqualTo(2));
    }

    /// <summary>
    /// Verifies that HasPreviousPage is false for the first page.
    /// </summary>
    [Test]
    public void HasPreviousPage_OnFirstPage_ShouldReturnFalse()
    {
        var page = new Page<string>(
            ["One"],
            1,
            10);

        var pagedList = new PagedList<string>(
            page,
            25);

        Assert.That(
            pagedList.HasPreviousPage,
            Is.False);
    }

    /// <summary>
    /// Verifies that HasPreviousPage is true after the first page.
    /// </summary>
    [Test]
    public void HasPreviousPage_AfterFirstPage_ShouldReturnTrue()
    {
        var page = new Page<string>(
            ["One"],
            2,
            10);

        var pagedList = new PagedList<string>(
            page,
            25);

        Assert.That(
            pagedList.HasPreviousPage,
            Is.True);
    }

    /// <summary>
    /// Verifies that HasNextPage is true when another page exists.
    /// </summary>
    [Test]
    public void HasNextPage_WhenAnotherPageExists_ShouldReturnTrue()
    {
        var page = new Page<string>(
            ["One"],
            1,
            10);

        var pagedList = new PagedList<string>(
            page,
            25);

        Assert.That(
            pagedList.HasNextPage,
            Is.True);
    }

    /// <summary>
    /// Verifies that HasNextPage is false on the last page.
    /// </summary>
    [Test]
    public void HasNextPage_OnLastPage_ShouldReturnFalse()
    {
        var page = new Page<string>(
            ["One"],
            3,
            10);

        var pagedList = new PagedList<string>(
            page,
            25);

        Assert.That(
            pagedList.HasNextPage,
            Is.False);
    }

    /// <summary>
    /// Verifies that IsFirstPage is true only for the first page.
    /// </summary>
    [Test]
    public void IsFirstPage_OnFirstPage_ShouldReturnTrue()
    {
        var page = new Page<string>(
            ["One"],
            1,
            10);

        var pagedList = new PagedList<string>(
            page,
            25);

        Assert.That(
            pagedList.IsFirstPage,
            Is.True);
    }

    /// <summary>
    /// Verifies that IsFirstPage is false after the first page.
    /// </summary>
    [Test]
    public void IsFirstPage_AfterFirstPage_ShouldReturnFalse()
    {
        var page = new Page<string>(
            ["One"],
            2,
            10);

        var pagedList = new PagedList<string>(
            page,
            25);

        Assert.That(
            pagedList.IsFirstPage,
            Is.False);
    }

    /// <summary>
    /// Verifies that IsLastPage is true on the last page.
    /// </summary>
    [Test]
    public void IsLastPage_OnLastPage_ShouldReturnTrue()
    {
        var page = new Page<string>(
            ["One"],
            3,
            10);

        var pagedList = new PagedList<string>(
            page,
            25);

        Assert.That(
            pagedList.IsLastPage,
            Is.True);
    }

    /// <summary>
    /// Verifies that IsLastPage is false when another page exists.
    /// </summary>
    [Test]
    public void IsLastPage_WhenAnotherPageExists_ShouldReturnFalse()
    {
        var page = new Page<string>(
            ["One"],
            2,
            10);

        var pagedList = new PagedList<string>(
            page,
            25);

        Assert.That(
            pagedList.IsLastPage,
            Is.False);
    }

    #endregion
}
