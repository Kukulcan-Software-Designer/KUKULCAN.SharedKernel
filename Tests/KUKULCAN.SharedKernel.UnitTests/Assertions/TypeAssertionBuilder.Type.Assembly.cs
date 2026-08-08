using System.Globalization;
using FluentAssertions;

namespace KUKULCAN.SharedKernel.UnitTests.Assertions;

/// <summary>
/// Provides assertions related to assembly metadata,
/// namespaces and naming conventions.
/// </summary>
public partial class TypeAssertionBuilder
{
    #region Assembly

    /// <summary>
    /// Verifies that the type belongs to the specified assembly.
    /// </summary>
    public TypeAssertionBuilder BeInAssembly<TAssemblyMarker>()
    {
        Type.Assembly
            .Should()
            .BeSameAs(
                typeof(TAssemblyMarker).Assembly,
                $"{Type.FullName} should belong to assembly {typeof(TAssemblyMarker).Assembly.GetName().Name}.");

        return this;
    }

    /// <summary>
    /// Verifies that the type belongs to the specified assembly.
    /// </summary>
    public TypeAssertionBuilder HaveAssembly(string assemblyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(assemblyName);

        Type.Assembly.GetName().Name
            .Should()
            .Be(assemblyName);

        return this;
    }

    /// <summary>
    /// Verifies that the type belongs to an assembly whose name starts with the specified prefix.
    /// </summary>
    public TypeAssertionBuilder HaveAssemblyStartingWith(string prefix)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(prefix);

        Type.Assembly.GetName().Name!
            .Should()
            .StartWith(prefix);

        return this;
    }

    /// <summary>
    /// Verifies that the assembly is strongly signed.
    /// </summary>
    public TypeAssertionBuilder BeStrongNamed()
    {
        Type.Assembly
            .GetName()
            .GetPublicKeyToken()
            .Should()
            .NotBeNull();

        Type.Assembly
            .GetName()
            .GetPublicKeyToken()!
            .Length
            .Should()
            .BeGreaterThan(0);

        return this;
    }

    #endregion

    #region Version

    /// <summary>
    /// Verifies the assembly version.
    /// </summary>
    public TypeAssertionBuilder HaveAssemblyVersion(Version version)
    {
        ArgumentNullException.ThrowIfNull(version);

        Type.Assembly
            .GetName()
            .Version
            .Should()
            .Be(version);

        return this;
    }

    /// <summary>
    /// Verifies the assembly version.
    /// </summary>
    public TypeAssertionBuilder HaveAssemblyVersion(
        int major,
        int minor,
        int build,
        int revision)
    {
        return HaveAssemblyVersion(
            new Version(
                major,
                minor,
                build,
                revision));
    }

    #endregion

    #region Namespace

    /// <summary>
    /// Verifies the namespace.
    /// </summary>
    public TypeAssertionBuilder HaveNamespace(string @namespace)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(@namespace);

        Type.Namespace
            .Should()
            .Be(@namespace);

        return this;
    }

    /// <summary>
    /// Verifies that the namespace starts with the specified prefix.
    /// </summary>
    public TypeAssertionBuilder HaveNamespaceStartingWith(string prefix)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(prefix);

        Type.Namespace
            .Should()
            .StartWith(prefix);

        return this;
    }

    /// <summary>
    /// Verifies that the namespace ends with the specified suffix.
    /// </summary>
    public TypeAssertionBuilder HaveNamespaceEndingWith(string suffix)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(suffix);

        Type.Namespace
            .Should()
            .EndWith(suffix);

        return this;
    }

    #endregion

    #region Name

    /// <summary>
    /// Verifies the CLR type name.
    /// </summary>
    public TypeAssertionBuilder HaveName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Type.Name
            .Should()
            .Be(name);

        return this;
    }

    /// <summary>
    /// Verifies the CLR full name.
    /// </summary>
    public TypeAssertionBuilder HaveFullName(string fullName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fullName);

        Type.FullName
            .Should()
            .Be(fullName);

        return this;
    }

    /// <summary>
    /// Verifies that the type name starts with the specified prefix.
    /// </summary>
    public TypeAssertionBuilder HaveNameStartingWith(string prefix)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(prefix);

        Type.Name
            .Should()
            .StartWith(prefix);

        return this;
    }

    /// <summary>
    /// Verifies that the type name ends with the specified suffix.
    /// </summary>
    public TypeAssertionBuilder HaveNameEndingWith(string suffix)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(suffix);

        Type.Name
            .Should()
            .EndWith(suffix);

        return this;
    }

    /// <summary>
    /// Verifies that the type name matches the specified regular expression.
    /// </summary>
    public TypeAssertionBuilder MatchName(string regularExpression)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(regularExpression);

        System.Text.RegularExpressions.Regex
            .IsMatch(Type.Name, regularExpression)
            .Should()
            .BeTrue();

        return this;
    }

    #endregion

    #region Culture

    /// <summary>
    /// Verifies the assembly culture.
    /// </summary>
    public TypeAssertionBuilder HaveCulture(string cultureName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cultureName);

        Type.Assembly
            .GetName()
            .CultureInfo!
            .Name
            .Should()
            .Be(cultureName);

        return this;
    }

    /// <summary>
    /// Verifies that the assembly is culture neutral.
    /// </summary>
    public TypeAssertionBuilder BeCultureNeutral()
    {
        Type.Assembly
            .GetName()
            .CultureInfo
            .Should()
            .Be(CultureInfo.InvariantCulture);

        return this;
    }

    #endregion
}
