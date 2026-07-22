using System.Reflection;
using KUKULCAN.SharedKernel.Validations.Internals;

namespace KUKULCAN.SharedKernel.Validations.Internals;

/// <summary>
/// Provides helper methods for extracting property information from lambda expressions.
/// </summary>
internal static class PropertyNameHelper
{
    /// <summary>
    /// Gets the property represented by the specified expression.
    /// </summary>
    /// <typeparam name="T">
    /// Source type.
    /// </typeparam>
    /// <param name="expression">
    /// Property expression.
    /// </param>
    /// <returns>
    /// The reflected property.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// The expression is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The expression does not represent a readable property.
    /// </exception>
    public static PropertyInfo GetProperty<T>(Expression<Func<T, object?>> expression)
    {
        ArgumentNullException.ThrowIfNull(expression);
        Expression body = expression.Body;

        if (body is UnaryExpression unary &&
            unary.NodeType == ExpressionType.Convert)
        {
            body = unary.Operand;
        }
        if (body is not MemberExpression member || member.Member is not PropertyInfo property)
        {
            throw new ArgumentException(
                ValidationInternalMessages.ExpressionMustReferenceProperty(),
                nameof(expression));

        }
        if (property.GetMethod is null)
        {
            throw new ArgumentException(
                ValidationInternalMessages.PropertyMustBeReadable(),
                nameof(expression));
        }

        return property;
    }

    /// <summary>
    /// Gets the property name represented by the specified expression.
    /// </summary>
    public static string GetPropertyName<T>(Expression<Func<T, object?>> expression)
    {
        return GetProperty(expression).Name;
    }
}
