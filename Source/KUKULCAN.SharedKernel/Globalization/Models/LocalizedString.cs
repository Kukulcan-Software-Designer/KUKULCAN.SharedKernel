namespace KUKULCAN.SharedKernel.Globalization.Models;

public sealed record LocalizedString(string Key, string Value, bool ResourceNotFound);
