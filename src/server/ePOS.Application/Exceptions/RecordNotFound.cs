namespace ePOS.Application.Exceptions;

public class RecordNotFound : BadRequestException
{
    public RecordNotFound(string recordName, Guid? recordId = null) : base( $"{recordName}NotFound {recordId}")
    {
        
    }
}