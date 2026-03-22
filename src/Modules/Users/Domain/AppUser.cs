using BuildingBlocks.Domain.Common;
using BuildingBlocks.Domain.Enums;

namespace Users.Domain;

public sealed class AppUser : AggregateRoot
{
    private readonly List<UserRole> _roles = new();

    public string FullName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public bool IsKycVerified { get; private set; }
    public string? AvatarUrl { get; private set; }
    public IReadOnlyCollection<UserRole> Roles => _roles;

    private AppUser() { }

    public AppUser(string fullName, string email, string phoneNumber, string passwordHash, IEnumerable<RoleType> roles)
    {
        FullName = fullName;
        Email = email.ToLowerInvariant();
        PhoneNumber = phoneNumber;
        PasswordHash = passwordHash;
        foreach (var role in roles.Distinct())
        {
            _roles.Add(new UserRole(Id, role));
        }
    }

    public void UpdateProfile(string fullName, string phoneNumber, string? avatarUrl)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
        AvatarUrl = avatarUrl;
        Touch();
    }

    public void VerifyKyc()
    {
        IsKycVerified = true;
        Touch();
    }
}
