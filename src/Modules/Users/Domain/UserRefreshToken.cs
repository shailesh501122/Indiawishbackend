using BuildingBlocks.Domain.Common;

namespace Users.Domain;

public sealed class UserRefreshToken : BaseEntity
{
    public Guid UserId { get; private set; }
    public string Token { get; private set; } = string.Empty;
    public DateTime ExpiresOnUtc { get; private set; }
    public bool IsRevoked { get; private set; }

    private UserRefreshToken() { }

    public UserRefreshToken(Guid userId, string token, DateTime expiresOnUtc)
    {
        UserId = userId;
        Token = token;
        ExpiresOnUtc = expiresOnUtc;
    }

    public void Revoke()
    {
        IsRevoked = true;
        Touch();
    }
}
