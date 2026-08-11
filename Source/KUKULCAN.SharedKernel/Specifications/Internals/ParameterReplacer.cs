namespace KUKULCAN.SharedKernel.Specifications.Internals;

/// <summary>
/// Replaces one parameter expression with another.
/// </summary>
internal sealed class ParameterReplacer : ExpressionVisitor
{
    private readonly ParameterExpression _source;
    private readonly ParameterExpression _target;

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterReplacer"/> class.
    /// </summary>
    /// <param name="source">
    /// Parameter to replace.
    /// </param>
    /// <param name="target">
    /// Replacement parameter.
    /// </param>
    internal ParameterReplacer(ParameterExpression source, ParameterExpression target)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(target);

        _source = source;
        _target = target;
    }

    /// <inheritdoc/>
    protected override Expression VisitParameter(ParameterExpression node)
    {
        ArgumentNullException.ThrowIfNull(node);

        return node == _source ? _target : base.VisitParameter(node);
    }

    /// <summary>
    /// Replaces a parameter inside an expression.
    /// </summary>
    /// <param name="expression">
    /// Expression to rewrite.
    /// </param>
    /// <param name="source">
    /// Parameter to replace.
    /// </param>
    /// <param name="target">
    /// Replacement parameter.
    /// </param>
    /// <returns>
    /// A rewritten expression using the replacement parameter.
    /// </returns>
    internal static Expression Replace(Expression expression, ParameterExpression source, ParameterExpression target)
    {
        ArgumentNullException.ThrowIfNull(expression);
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(target);

        return new ParameterReplacer(source, target).Visit(expression)!;
    }
}
