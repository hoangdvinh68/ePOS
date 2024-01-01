namespace ePOS.Domain;

public interface IAuditableEntity : IEntity
{
    public DateTimeOffset CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTimeOffset? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }
}

public class AuditableEntity : Entity, IAuditableEntity
{
    public DateTimeOffset CreatedAt { get; set; }
    
    public Guid? CreatedBy { get; set; }
    
    public DateTimeOffset? ModifiedAt { get; set; }
    
    public Guid? ModifiedBy { get; set; }

    public virtual void SetCreationTracking(Guid tenantId, Guid? userId)
    {
        CreatedAt = DateTimeOffset.UtcNow;
        TenantId = tenantId;
        CreatedBy = userId;
    }

    public virtual void SetModificationTracking(Guid? userId)
    {
        ModifiedAt = DateTimeOffset.UtcNow;
        ModifiedBy = userId;
    }
}