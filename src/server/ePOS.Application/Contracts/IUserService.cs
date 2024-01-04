using ePOS.Application.Features.User.Commands;
using ePOS.Application.Features.User.Queries;
using ePOS.Application.Features.User.Responses;
using ePOS.Application.ValueObjects;

namespace ePOS.Application.Contracts;

public interface IUserService
{
    Task<SignInResponse> SignInAsync(SignInCommand command, CancellationToken cancellationToken);
    Task<SignUpResponse> SignUpAsync(SignUpCommand command, CancellationToken cancellationToken);
    UserClaimsValue GetUserClaimsValue();
    Task<GetProfileResponse> GetProfileAsync(GetProfileQuery query, CancellationToken cancellationToken);
}