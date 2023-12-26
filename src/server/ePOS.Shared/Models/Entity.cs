using System.ComponentModel.DataAnnotations;

namespace ePOS.Shared.Models;

public interface IEntity
{
    public Guid Id { get; set; }
    
    public long SubId { get; set; }
}

public interface IAuditableEntity : IEntity
{
    public DateTimeOffset CreatedAt { get; set; }
    
    public Guid? CreatedBy { get; set; }
    
    public DateTimeOffset? ModifiedAt { get; set; }
    
    public Guid? ModifiedBy { get; set; }

    void SetCreate(Guid? userId);
    
    void SetModify(Guid? userId);
}

public interface ISoftDeletableEntity
{
    public bool Deleted { get; set; }

    void SetDelete();
}


public class Entity : IEntity
{
    [Key]
    public Guid Id { get; set; }
    
    public long SubId { get; set; }
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