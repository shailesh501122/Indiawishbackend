using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuildingBlocks.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BuildingBlocks.Infrastructure.Identity;

public sealed class JwtTokenService(IConfiguration configuration) : IJwtTokenService
{
    public string GenerateAccessToken(Guid userId, string email, IEnumerable<string> roles)
    {
        var claims = new List<Claim> { new(JwtRegisteredClaimNames.Sub, userId.ToString()), new(JwtRegisteredClaimNames.Email, email) };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        var secret = configuration["Jwt:Secret"] ?? "super-secret-development-key-super-secret";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(configuration["Jwt:Issuer"] ?? "IndiaWish", configuration["Jwt:Audience"] ?? "IndiaWishClients", claims, expires: DateTime.UtcNow.AddHours(2), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken() => Convert.ToBase64String(Guid.NewGuid().ToByteArray());
}
