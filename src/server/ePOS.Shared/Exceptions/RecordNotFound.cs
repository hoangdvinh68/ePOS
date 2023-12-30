namespace ePOS.Shared.Exceptions;

public class RecordNotFound : BadRequestException
{
    public RecordNotFound(string recordName) : base( $"RecordNotFound: {recordName}")
    {
        
    }
}