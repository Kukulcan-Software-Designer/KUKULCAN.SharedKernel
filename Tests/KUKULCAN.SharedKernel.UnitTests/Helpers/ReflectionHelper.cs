using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers;

/// <summary>
/// Provides a high-level reflection service used by the
/// KUKULCAN.SharedKernel testing framework.
///
/// <para>
/// This class centralizes every reflection operation performed
/// by the testing infrastructure. Reflection must never be used
/// directly from test classes.
/// </para>
///
/// <para>
/// ReflectionHelper exposes specialized APIs through multiple
/// partial class files:
///
/// • Core
/// • Discovery
/// • Search
/// • Validation
/// • Assertions
/// </para>
///
/// <para>
/// Reflection metadata is cached to minimize allocations and
/// improve execution speed across large test suites.
/// </para>
/// </summary>
public partial class ReflectionHelper
{
    #region Fields

    /// <summary>
    /// Cache of public constructors.
    /// </summary>
    private readonly ConcurrentDictionary<Type, ConstructorInfo[]> _constructors = new();

    /// <summary>
    /// Cache of public methods.
    /// </summary>
    private readonly ConcurrentDictionary<Type, MethodInfo[]> _methods = new();

    /// <summary>
    /// Cache of public properties.
    /// </summary>
    private readonly ConcurrentDictionary<Type, PropertyInfo[]> _properties = new();

    /// <summary>
    /// Cache of public fields.
    /// </summary>
    private readonly ConcurrentDictionary<Type, FieldInfo[]> _fields = new();

    /// <summary>
    /// Cache of implemented interfaces.
    /// </summary>
    private readonly ConcurrentDictionary<Type, Type[]> _interfaces = new();

    /// <summary>
    /// Cache of attributes.
    /// </summary>
    private readonly ConcurrentDictionary<Type, Attribute[]> _attributes = new();

    /// <summary>
    /// Cache of nested types.
    /// </summary>
    private readonly ConcurrentDictionary<Type, Type[]> _nestedTypes = new();

    #endregion

    #region Internal Cache API

    /// <summary>
    /// Gets a cached value or creates it.
    /// </summary>
    protected TValue GetOrAdd<TKey, TValue>(ConcurrentDictionary<TKey, TValue> dictionary, TKey key,
        Func<TKey, TValue> factory) where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        ArgumentNullException.ThrowIfNull(factory);

        return dictionary.GetOrAdd(key, factory);
    }

    /// <summary>
    /// Removes every cached reflection entry.
    /// </summary>
    public void ClearCache()
    {
        _constructors.Clear();
        _methods.Clear();
        _properties.Clear();
        _fields.Clear();
        _interfaces.Clear();
        _attributes.Clear();
        _nestedTypes.Clear();
    }

    /// <summary>
    /// Returns the number of cached constructor entries.
    /// </summary>
    public int CachedConstructors => _constructors.Count;

    /// <summary>
    /// Returns the number of cached method entries.
    /// </summary>
    public int CachedMethods => _methods.Count;

    /// <summary>
    /// Returns the number of cached property entries.
    /// </summary>
    public int CachedProperties => _properties.Count;

    /// <summary>
    /// Returns the number of cached field entries.
    /// </summary>
    public int CachedFields => _fields.Count;

    /// <summary>
    /// Returns the number of cached interface entries.
    /// </summary>
    public int CachedInterfaces => _interfaces.Count;

    /// <summary>
    /// Returns the number of cached attribute entries.
    /// </summary>
    public int CachedAttributes => _attributes.Count;

    /// <summary>
    /// Returns the total number of cached reflection entries.
    /// </summary>
    public int CachedEntries =>
        CachedConstructors + CachedMethods + CachedProperties +
        CachedFields + CachedInterfaces + CachedAttributes;

    #endregion

    #region Reflection Flags

    /// <summary>
    /// Default flags used to discover public instance members.
    /// </summary>
    protected const BindingFlags PublicInstance = BindingFlags.Instance | BindingFlags.Public;

    /// <summary>
    /// Default flags used to discover public members.
    /// </summary>
    protected const BindingFlags PublicStatic = BindingFlags.Static | BindingFlags.Public;

    /// <summary>
    /// Default flags used to discover every public member.
    /// </summary>
    protected const BindingFlags Public = PublicInstance | PublicStatic;

    /// <summary>
    /// Default flags used to discover every member.
    /// </summary>
    protected const BindingFlags All = BindingFlags.Instance | BindingFlags.Static |
                                       BindingFlags.Public | BindingFlags.NonPublic;
    private static string BuildConstructorSignature(ConstructorInfo constructor)
    {
        ArgumentNullException.ThrowIfNull(constructor);

        return string.Join(", ", constructor.GetParameters().Select(p => p.ParameterType.Name));
    }

    #endregion
}
