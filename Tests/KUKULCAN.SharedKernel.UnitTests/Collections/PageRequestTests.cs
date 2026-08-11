using KUKULCAN.SharedKernel.Collections;

namespace KUKULCAN.SharedKernel.UnitTests.Collections;

/// <summary>
/// Contains unit tests for <see cref="PageRequest"/>.
/// </summary>
[TestFixture]
public sealed class PageRequestTests
{
    #region Construction

    /// <summary>
    /// Verifies that the default constructor values are applied.
    /// </summary>
    [Test]
    public void Constructor_WithDefaults_ShouldUseDefaultValues()
    {
        var request = new PageRequest();

        Assert.Multiple(() =>
        {
            Assert.That(request.PageNumber, Is.EqualTo(1));
            Assert.That(
                request.PageSize,
                Is.EqualTo(PageRequest.DefaultPageSize));
        });
    }

    /// <summary>
    /// Verifies that explicit paging values are preserved.
    /// </summary>
    [Test]
    public void Constructor_WithValidValues_ShouldAssignProperties()
    {
        var request = new PageRequest(3, 50);

        Assert.Multiple(() =>
        {
            Assert.That(request.PageNumber, Is.EqualTo(3));
            Assert.That(request.PageSize, Is.EqualTo(50));
        });
    }

    /// <summary>
    /// Verifies that page number zero is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithZeroPageNumber_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.That(
            () => new PageRequest(0, 25),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    /// <summary>
    /// Verifies that a negative page number is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithNegativePageNumber_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.That(
            () => new PageRequest(-1, 25),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    /// <summary>
    /// Verifies that page size zero is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithZeroPageSize_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.That(
            () => new PageRequest(1, 0),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    /// <summary>
    /// Verifies that a negative page size is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithNegativePageSize_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.That(
            () => new PageRequest(1, -1),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    /// <summary>
    /// Verifies that a page size greater than the maximum is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithPageSizeAboveMaximum_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.That(
            () => new PageRequest(1, PageRequest.MaximumPageSize + 1),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    /// <summary>
    /// Verifies that the maximum allowed page size is accepted.
    /// </summary>
    [Test]
    public void Constructor_WithMaximumPageSize_ShouldAcceptValue()
    {
        var request = new PageRequest(1, PageRequest.MaximumPageSize);

        Assert.That(
            request.PageSize,
            Is.EqualTo(PageRequest.MaximumPageSize));
    }

    #endregion

    #region Constants

    /// <summary>
    /// Verifies the default page size constant.
    /// </summary>
    [Test]
    public void DefaultPageSize_ShouldBeTwentyFive()
    {
        Assert.That(
            PageRequest.DefaultPageSize,
            Is.EqualTo(25));
    }

    /// <summary>
    /// Verifies the maximum page size constant.
    /// </summary>
    [Test]
    public void MaximumPageSize_ShouldBeOneHundred()
    {
        Assert.That(
            PageRequest.MaximumPageSize,
            Is.EqualTo(100));
    }

    #endregion
}
