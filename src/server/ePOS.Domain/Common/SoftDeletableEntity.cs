namespace ePOS.Domain.Common;

public interface ISoftDeletableEntity
{
    public bool Deleted { get; set; }

    void SetDelete();
}