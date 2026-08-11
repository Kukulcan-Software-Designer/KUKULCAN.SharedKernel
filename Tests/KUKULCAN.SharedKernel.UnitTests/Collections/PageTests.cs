using KUKULCAN.SharedKernel.Collections;

namespace KUKULCAN.SharedKernel.UnitTests.Collections;

/// <summary>
/// Contains unit tests for <see cref="Page{T}"/>.
/// </summary>
[TestFixture]
public sealed class PageTests
{
    #region Construction

    /// <summary>
    /// Verifies that valid constructor arguments are assigned.
    /// </summary>
    [Test]
    public void Constructor_WithValidValues_ShouldAssignProperties()
    {
        IReadOnlyList<string> items = ["One", "Two"];

        var page = new Page<string>(
            items,
            2,
            10);

        Assert.Multiple(() =>
        {
            Assert.That(page.Items, Is.SameAs(items));
            Assert.That(page.PageNumber, Is.EqualTo(2));
            Assert.That(page.PageSize, Is.EqualTo(10));
        });
    }

    /// <summary>
    /// Verifies that a null item collection is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithNullItems_ShouldThrowArgumentNullException()
    {
        Assert.That(
            () => new Page<string>(null!, 1, 10),
            Throws.ArgumentNullException);
    }

    /// <summary>
    /// Verifies that page number zero is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithZeroPageNumber_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.That(
            () => new Page<string>([], 0, 10),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    /// <summary>
    /// Verifies that a negative page number is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithNegativePageNumber_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.That(
            () => new Page<string>([], -1, 10),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    /// <summary>
    /// Verifies that page size zero is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithZeroPageSize_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.That(
            () => new Page<string>([], 1, 0),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    /// <summary>
    /// Verifies that a negative page size is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithNegativePageSize_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.That(
            () => new Page<string>([], 1, -1),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    #endregion

    #region Computed properties

    /// <summary>
    /// Verifies that Count returns the number of items.
    /// </summary>
    [Test]
    public void Count_ShouldReturnNumberOfItems()
    {
        var page = new Page<string>(
            ["One", "Two", "Three"],
            1,
            10);

        Assert.That(page.Count, Is.EqualTo(3));
    }

    /// <summary>
    /// Verifies that HasItems is true when the page contains items.
    /// </summary>
    [Test]
    public void HasItems_WithItems_ShouldReturnTrue()
    {
        var page = new Page<string>(
            ["One"],
            1,
            10);

        Assert.That(page.HasItems, Is.True);
    }

    /// <summary>
    /// Verifies that HasItems is false when the page is empty.
    /// </summary>
    [Test]
    public void HasItems_WithNoItems_ShouldReturnFalse()
    {
        var page = new Page<string>(
            [],
            1,
            10);

        Assert.That(page.HasItems, Is.False);
    }

    /// <summary>
    /// Verifies that IsEmpty is false when the page contains items.
    /// </summary>
    [Test]
    public void IsEmpty_WithItems_ShouldReturnFalse()
    {
        var page = new Page<string>(
            ["One"],
            1,
            10);

        Assert.That(page.IsEmpty, Is.False);
    }

    /// <summary>
    /// Verifies that IsEmpty is true when the page contains no items.
    /// </summary>
    [Test]
    public void IsEmpty_WithNoItems_ShouldReturnTrue()
    {
        var page = new Page<string>(
            [],
            1,
            10);

        Assert.That(page.IsEmpty, Is.True);
    }

    #endregion
}
