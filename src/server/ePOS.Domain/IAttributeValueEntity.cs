namespace ePOS.Domain;

public interface IAttributeValueEntity : IEntity
{
    public string Attribute { get; set; }
    
    public string Value { get; set; }
}