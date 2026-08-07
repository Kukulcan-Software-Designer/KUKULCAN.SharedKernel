using AutoFixture;
using AutoFixture.AutoMoq;
using KUKULCAN.SharedKernel.UnitTests.Helpers;
using Moq;

namespace KUKULCAN.SharedKernel.UnitTests.Infrastructure;

/// <summary>
/// Root class for every unit test in KUKULCAN.SharedKernel.
///
/// This class intentionally contains almost no logic.
/// Its responsibility is to bootstrap the testing infrastructure
/// and expose the common providers used by derived test classes.
///
/// Business-specific helpers belong to specialized providers,
/// never to this class.
/// </summary>
[TestFixture]
public abstract class TestBase
{
    private IFixture? _fixture;

    /// <summary>
    /// Gets the AutoFixture instance.
    /// </summary>
    protected IFixture Fixture => _fixture ??= CreateFixture();

    /// <summary>
    /// Gets the reflection helper.
    /// </summary>
    protected ReflectionHelper Reflection { get; } = new();

    /// <summary>
    /// Gets the serialization helper.
    /// </summary>
    protected SerializationHelper Serialization { get; } = new();

    /// <summary>
    /// Gets the random data helper.
    /// </summary>
    protected RandomDataHelper Random { get; } = new();

    /// <summary>
    /// Gets the equality helper.
    /// </summary>
    protected EqualityHelper Equality { get; } = new();

    /// <summary>
    /// Gets the hash code helper.
    /// </summary>
    protected HashCodeHelper HashCodes { get; } = new();

    /// <summary>
    /// Gets the thread helper.
    /// </summary>
    protected ThreadHelper Threads { get; } = new();

    /// <summary>
    /// Gets the assertion helper.
    /// </summary>
    protected AssertionHelper Assertions { get; } = new();

    #region NUnit lifecycle

    [OneTimeSetUp]
    public virtual void OneTimeSetUp()
    {
    }

    [SetUp]
    public virtual void SetUp()
    {
    }

    [TearDown]
    public virtual void TearDown()
    {
    }

    [OneTimeTearDown]
    public virtual void OneTimeTearDown()
    {
    }

    #endregion

    #region Fixture

    protected virtual IFixture CreateFixture()
    {
        var fixture = new Fixture();

        fixture.Customize(
            new AutoMoqCustomization
            {
                ConfigureMembers = true,
                GenerateDelegates = true
            });

        ConfigureFixture(fixture);

        return fixture;
    }

    /// <summary>
    /// Allows derived classes to customize AutoFixture.
    /// </summary>
    protected virtual void ConfigureFixture(IFixture fixture)
    {
    }

    #endregion

    #region Object creation

    protected T Create<T>() => Fixture.Create<T>();

    protected IReadOnlyCollection<T> CreateMany<T>(int count = 3) => Fixture.CreateMany<T>(count).ToArray();

    protected T Freeze<T>() where T : class => Fixture.Freeze<T>();

    protected Mock<T> FreezeMock<T>() where T : class => Fixture.Freeze<Mock<T>>();

    protected Mock<T> CreateMock<T>() where T : class => new(MockBehavior.Strict);

    #endregion

    #region SUT creation

    protected virtual TSut CreateSut<TSut>() where TSut : class => Fixture.Create<TSut>();

    #endregion
}
