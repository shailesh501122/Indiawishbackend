using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Interfaces;
using BuildingBlocks.Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Users.Domain;

namespace Users.Application;

public sealed record RegisterUserCommand(string FullName, string Email, string PhoneNumber, string Password, IReadOnlyCollection<RoleType> Roles) : ICommand<AuthResponse>;
public sealed record AuthResponse(Guid UserId, string AccessToken, string RefreshToken);

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).MinimumLength(8);
        RuleFor(x => x.Roles).NotEmpty();
    }
}

public sealed class RegisterUserCommandHandler(IApplicationDbContext dbContext, IJwtTokenService jwtTokenService) : MediatR.IRequestHandler<RegisterUserCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.ToLowerInvariant();
        if (await dbContext.Users.AnyAsync(user => user.Email == normalizedEmail, cancellationToken))
            throw new InvalidOperationException("User already exists.");

        var passwordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(request.Password));
        var user = new AppUser(request.FullName, normalizedEmail, request.PhoneNumber, passwordHash, request.Roles);
        await dbContext.Users.AddAsync(user, cancellationToken);

        var accessToken = jwtTokenService.GenerateAccessToken(user.Id, user.Email, user.Roles.Select(role => role.Role.ToString()));
        var refreshTokenValue = jwtTokenService.GenerateRefreshToken();
        await dbContext.RefreshTokens.AddAsync(new UserRefreshToken(user.Id, refreshTokenValue, DateTime.UtcNow.AddDays(30)), cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new AuthResponse(user.Id, accessToken, refreshTokenValue);
    }
}
