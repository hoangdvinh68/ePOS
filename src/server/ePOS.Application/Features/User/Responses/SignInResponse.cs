namespace ePOS.Application.Features.User.Responses;

public class SignInResponse
{
    public string AccessToken { get; set; } = default!;

    public string RefreshToken { get; set; } = default!;

    public DateTime Expires { get; set; } = default!;
}