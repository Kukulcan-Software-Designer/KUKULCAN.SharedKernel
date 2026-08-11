using KUKULCAN.SharedKernel.Abstractions;

namespace KUKULCAN.SharedKernel.Time.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IClock"/>.
/// </summary>
public static class ClockExtensions
{
    extension(IClock clock)
    {
        /// <summary>
        /// Gets today's date in UTC.
        /// </summary>
        /// <returns>
        /// The current <see cref="DateOnly"/>.
        /// </returns>
        public DateOnly Today()
        {
            ArgumentNullException.ThrowIfNull(clock);

            return DateOnly.FromDateTime(clock.UtcNow.UtcDateTime);
        }

        /// <summary>
        /// Gets the current UTC time of day.
        /// </summary>
        /// <returns>
        /// The current <see cref="TimeOnly"/>.
        /// </returns>
        public TimeOnly CurrentTime()
        {
            ArgumentNullException.ThrowIfNull(clock);

            return TimeOnly.FromDateTime(clock.UtcNow.UtcDateTime);
        }

        /// <summary>
        /// Determines whether today is Saturday or Sunday.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if today is a weekend; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        public bool IsWeekend()
        {
            ArgumentNullException.ThrowIfNull(clock);

            return clock.UtcNow.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
        }

        /// <summary>
        /// Determines whether today is a weekday.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if today is Monday through Friday;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool IsWeekday()
        {
            ArgumentNullException.ThrowIfNull(clock);

            return !clock.IsWeekend();
        }

        /// <summary>
        /// Determines whether the specified date is today.
        /// </summary>
        /// <param name="date">
        /// Date to compare.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the specified date is today;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool IsToday(DateTimeOffset date)
        {
            ArgumentNullException.ThrowIfNull(clock);

            return date.UtcDateTime.Date == clock.UtcNow.UtcDateTime.Date;
        }

        /// <summary>
        /// Determines whether the specified instant is in the future.
        /// </summary>
        /// <param name="date">
        /// Date to compare.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the date is in the future;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool IsFuture(DateTimeOffset date)
        {
            ArgumentNullException.ThrowIfNull(clock);

            return date > clock.UtcNow;
        }

        /// <summary>
        /// Determines whether the specified instant is in the past.
        /// </summary>
        /// <param name="date">
        /// Date to compare.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the date is in the past;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool IsPast(DateTimeOffset date)
        {
            ArgumentNullException.ThrowIfNull(clock);

            return date < clock.UtcNow;
        }
    }
}
