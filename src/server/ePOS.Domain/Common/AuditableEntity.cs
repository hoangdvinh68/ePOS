namespace ePOS.Domain.Common;

public interface IAuditableEntity : IEntity
{
    public DateTimeOffset CreatedAt { get; set; }
    
    public Guid? CreatedBy { get; set; }
    
    public DateTimeOffset? ModifiedAt { get; set; }
    
    public Guid? ModifiedBy { get; set; }

    void SetCreate(Guid? userId);
    
    void SetModify(Guid? userId);
}

public class AuditableEntity : Entity, IAuditableEntity
{
    public DateTimeOffset CreatedAt { get; set; }
    
    public Guid? CreatedBy { get; set; }
    
    public DateTimeOffset? ModifiedAt { get; set; }
    
    public Guid? ModifiedBy { get; set; }
    
    public void SetCreate(Guid? userId)
    {
        CreatedAt = DateTimeOffset.UtcNow;
        CreatedBy = userId;
    }

    public void SetModify(Guid? userId)
    {
        ModifiedAt = DateTimeOffset.UtcNow;
        ModifiedBy = userId;
    }
}