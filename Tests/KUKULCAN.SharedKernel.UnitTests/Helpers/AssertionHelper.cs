using System;
using System.Collections.Generic;
using FluentAssertions;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers;

/// <summary>
/// Provides common assertion helpers used across unit tests.
/// </summary>
public sealed class AssertionHelper
{
    /// <summary>
    /// Verifies that the specified value is not null.
    /// </summary>
    public static void ShouldNotBeNull<T>(T? value)
    {
        value.Should().NotBeNull();
    }

    /// <summary>
    /// Verifies that the specified value is null.
    /// </summary>
    public static void ShouldBeNull<T>(T? value)
    {
        value.Should().BeNull();
    }

    /// <summary>
    /// Verifies that two values are equal.
    /// </summary>
    public static void ShouldBe<T>(
        T actual,
        T expected)
    {
        actual.Should().Be(expected);
    }

    /// <summary>
    /// Verifies that two values are not equal.
    /// </summary>
    public static void ShouldNotBe<T>(
        T actual,
        T unexpected)
    {
        actual.Should().NotBe(unexpected);
    }

    /// <summary>
    /// Verifies that a condition is true.
    /// </summary>
    public static void ShouldBeTrue(bool value)
    {
        value.Should().BeTrue();
    }

    /// <summary>
    /// Verifies that a condition is false.
    /// </summary>
    public static void ShouldBeFalse(bool value)
    {
        value.Should().BeFalse();
    }

    /// <summary>
    /// Verifies that a sequence is empty.
    /// </summary>
    public static void ShouldBeEmpty<T>(
        IEnumerable<T> values)
    {
        values.Should().BeEmpty();
    }

    /// <summary>
    /// Verifies that a sequence is not empty.
    /// </summary>
    public static void ShouldNotBeEmpty<T>(
        IEnumerable<T> values)
    {
        values.Should().NotBeEmpty();
    }

    /// <summary>
    /// Verifies that the specified action throws the expected exception.
    /// </summary>
    public static void ShouldThrow<TException>(
        Action action)
        where TException : Exception
    {
        action.Should().Throw<TException>();
    }

    /// <summary>
    /// Verifies that the specified action does not throw.
    /// </summary>
    public static void ShouldNotThrow(
        Action action)
    {
        action.Should().NotThrow();
    }
}
