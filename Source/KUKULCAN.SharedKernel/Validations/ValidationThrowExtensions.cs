using KUKULCAN.SharedKernel.Exceptions;

namespace KUKULCAN.SharedKernel.Validations;

/// <summary>
/// Provides extension methods for <see cref="ValidationResult"/>.
/// </summary>
public static class ValidationThrowExtensions
{
    /// <param name="validationResult">
    /// Validation result.
    /// </param>
    extension(ValidationResult validationResult)
    {
        /// <summary>
        /// Throws a <see cref="ValidationException"/> if the validation result is invalid.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="validationResult"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ValidationException">
        /// The validation result is invalid.
        /// </exception>
        public void ThrowIfInvalid()
        {
            validationResult.ThrowIfInvalid((null));
        }

        /// <summary>
        /// Throws a <see cref="ValidationException"/> if the validation result is invalid.
        /// </summary>
        /// <param name="innerException">
        /// Inner exception.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="validationResult"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ValidationException">
        /// The validation result is invalid.
        /// </exception>
        public void ThrowIfInvalid(Exception? innerException)
        {
            ArgumentNullException.ThrowIfNull(validationResult);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(
                    validationResult,
                    innerException);
            }
        }
    }
}
