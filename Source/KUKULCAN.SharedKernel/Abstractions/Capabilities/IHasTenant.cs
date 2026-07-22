namespace KUKULCAN.SharedKernel.Abstractions.Capabilities;

/// <summary>
/// Represents a tenant-aware entity.
/// </summary>
public interface IHasTenant
{
    Guid TenantId { get; set; }
}
