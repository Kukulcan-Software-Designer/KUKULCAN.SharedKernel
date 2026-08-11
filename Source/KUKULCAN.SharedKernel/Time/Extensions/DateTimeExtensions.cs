namespace KUKULCAN.SharedKernel.Time.Extensions;

/// <summary>
/// Provides extension methods for <see cref="DateTime"/>.
/// </summary>
public static class DateTimeExtensions
{
    extension(DateTime value)
    {
        /// <summary>
        /// Converts the current value to a <see cref="DateOnly"/>.
        /// </summary>
        /// <returns>
        /// The equivalent <see cref="DateOnly"/>.
        /// </returns>
        public DateOnly ToDateOnly()
        {
            return DateOnly.FromDateTime(value);
        }

        /// <summary>
        /// Converts the current value to a <see cref="TimeOnly"/>.
        /// </summary>
        /// <returns>
        /// The equivalent <see cref="TimeOnly"/>.
        /// </returns>
        public TimeOnly ToTimeOnly()
        {
            return TimeOnly.FromDateTime(value);
        }
    }
}
