using BuildingBlocks.Domain.Common;
using BuildingBlocks.Domain.Enums;

namespace Users.Domain;

public sealed class UserRole : BaseEntity
{
    public Guid UserId { get; private set; }
    public RoleType Role { get; private set; }

    private UserRole() { }

    public UserRole(Guid userId, RoleType role)
    {
        UserId = userId;
        Role = role;
    }
}
