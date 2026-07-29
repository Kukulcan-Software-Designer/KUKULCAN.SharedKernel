using System;

namespace KUKULCAN.SharedKernel.Specifications.Internals;

/// <summary>
/// Provides helper methods to combine specification expressions.
/// </summary>
internal static class ExpressionCombiner
{
    /// <summary>
    /// Combines two expressions using a logical AND.
    /// </summary>
    /// <typeparam name="T">
    /// Type evaluated by the expressions.
    /// </typeparam>
    /// <param name="left">
    /// Left expression.
    /// </param>
    /// <param name="right">
    /// Right expression.
    /// </param>
    /// <returns>
    /// A combined expression using <see cref="Expression.AndAlso(Expression, Expression)"/>.
    /// </returns>
    internal static Expression<Func<T, bool>> And<T>(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);
        (ParameterExpression parameter, Expression rightBody) = UnifyParameters(left, right);

        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left.Body, rightBody), parameter);
    }

    /// <summary>
    /// Combines two expressions using a logical OR.
    /// </summary>
    /// <typeparam name="T">
    /// Type evaluated by the expressions.
    /// </typeparam>
    /// <param name="left">
    /// Left expression.
    /// </param>
    /// <param name="right">
    /// Right expression.
    /// </param>
    /// <returns>
    /// A combined expression using <see cref="Expression.OrElse(Expression, Expression)"/>.
    /// </returns>
    internal static Expression<Func<T, bool>> Or<T>(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);
        (ParameterExpression parameter, Expression rightBody) = UnifyParameters(left, right);

        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left.Body, rightBody), parameter);
    }

    /// <summary>
    /// Negates an expression.
    /// </summary>
    /// <typeparam name="T">
    /// Type evaluated by the expression.
    /// </typeparam>
    /// <param name="expression">
    /// Expression to negate.
    /// </param>
    /// <returns>
    /// A negated expression.
    /// </returns>
    internal static Expression<Func<T, bool>> Not<T>(Expression<Func<T, bool>> expression)
    {
        ArgumentNullException.ThrowIfNull(expression);

        return Expression.Lambda<Func<T, bool>>(Expression.Not(expression.Body), expression.Parameters);
    }

    /// <summary>
    /// Prepares two expressions so they share the same parameter.
    /// </summary>
    /// <typeparam name="T">
    /// Type evaluated by the expressions.
    /// </typeparam>
    /// <param name="left">
    /// Left expression.
    /// </param>
    /// <param name="right">
    /// Right expression.
    /// </param>
    /// <returns>
    /// The unified parameter together with the rewritten right expression body.
    /// </returns>
    private static (ParameterExpression Parameter, Expression RightBody) UnifyParameters<T>(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        ParameterExpression parameter = left.Parameters[0];
        Expression rightBody = ParameterReplacer.Replace(right.Body, right.Parameters[0], parameter);

        return (parameter, rightBody);
    }
}
