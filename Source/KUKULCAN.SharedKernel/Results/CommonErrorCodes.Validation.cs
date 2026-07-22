namespace KUKULCAN.SharedKernel.Results;

/// <summary>
/// Defines standard validation error codes.
/// </summary>
public static partial class CommonErrorCodes
{
    /// <summary>
    /// One or more validation failures occurred.
    /// </summary>
    public const string ValidationFailed = "VALIDATION.FAILED";

    /// <summary>
    /// A required value is missing.
    /// </summary>
    public const string Required = "VALIDATION.REQUIRED";

    /// <summary>
    /// The value must be null.
    /// </summary>
    public const string Null = "VALIDATION.NULL";

    /// <summary>
    /// The value cannot be empty.
    /// </summary>
    public const string Empty = "VALIDATION.EMPTY";

    /// <summary>
    /// The value length is below the minimum allowed.
    /// </summary>
    public const string MinLength = "VALIDATION.LENGTH.MIN";

    /// <summary>
    /// The value length exceeds the maximum allowed.
    /// </summary>
    public const string MaxLength = "VALIDATION.LENGTH.MAX";

    /// <summary>
    /// The value length does not match the expected length.
    /// </summary>
    public const string ExactLength = "VALIDATION.LENGTH.EXACT_LENGTH";

    /// <summary>
    /// The value must be greater than the specified limit.
    /// </summary>
    public const string GreaterThan = "VALIDATION.NUMERIC.GREATER_THAN";

    /// <summary>
    /// The value must be greater than or equal to the specified limit.
    /// </summary>
    public const string GreaterThanOrEqual = "VALIDATION.NUMERIC.GREATER_THAN_OR_EQUAL";

    /// <summary>
    /// The value must be less than the specified limit.
    /// </summary>
    public const string LessThan = "VALIDATION.NUMERIC.LESS_THAN";

    /// <summary>
    /// The value must be less than or equal to the specified limit.
    /// </summary>
    public const string LessThanOrEqual = "VALIDATION.NUMERIC.LESS_THAN_OR_EQUAL";

    /// <summary>
    /// The value is outside the allowed range.
    /// </summary>
    public const string Between = "VALIDATION.NUMERIC.BETWEEN";

    /// <summary>
    /// The value has an invalid format.
    /// </summary>
    public const string InvalidFormat = "VALIDATION.PATTERN.FORMAT";

    /// <summary>
    /// The e-mail address is invalid.
    /// </summary>
    public const string InvalidEmail = "VALIDATION.PATTERN.EMAIL";

    /// <summary>
    /// The phone number is invalid.
    /// </summary>
    public const string InvalidPhone = "VALIDATION.PATTERN.PHONE";

    /// <summary>
    /// The URL is invalid.
    /// </summary>
    public const string InvalidUrl = "VALIDATION.PATTERN.URL";

    /// <summary>
    /// The value does not match the required pattern.
    /// </summary>
    public const string InvalidPattern = "VALIDATION.PATTERN.INVALID";

    /// <summary>
    /// The collection must be empty.
    /// </summary>
    public const string CollectionMustBeEmpty = "VALIDATION.COLLECTION.MUST_BE_EMPTY";

    /// <summary>
    /// The collection cannot be empty.
    /// </summary>
    public const string CollectionMustNotBeEmpty = "VALIDATION.COLLECTION.MUST_NOT_BE_EMPTY";

    /// <summary>
    /// The collection contains duplicate values.
    /// </summary>
    public const string Duplicate = "VALIDATION.COLLECTION.DUPLICATE";

    /// <summary>
    /// The collection contains an invalid item.
    /// </summary>
    public const string InvalidCollectionItem = "VALIDATION.COLLECTION.INVALID_ITEM";

    /// <summary>
    /// The date must be in the past.
    /// </summary>
    public const string PastDate = "VALIDATION.DATE.PAST";

    /// <summary>
    /// The date must be in the future.
    /// </summary>
    public const string FutureDate = "VALIDATION.DATE.FUTURE";

    /// <summary>
    /// The enumeration value is invalid.
    /// </summary>
    public const string InvalidEnum = "VALIDATION.ENUM.INVALID";
}
