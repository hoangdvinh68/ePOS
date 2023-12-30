namespace ePOS.Domain;

public interface ISoftDeletableEntity
{
    public bool IsDeleted { get; set; }
}