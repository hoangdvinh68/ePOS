namespace ePOS.Shared.Contracts;

public interface ITenantContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}