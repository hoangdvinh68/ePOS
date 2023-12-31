namespace ePOS.Application.Features.User.Responses;

public class SignUpResponse
{
    public Guid Id { get; set; }
    
    public string FullName { get; set; } = default!;

    public string Email { get; set; } = default!;
}