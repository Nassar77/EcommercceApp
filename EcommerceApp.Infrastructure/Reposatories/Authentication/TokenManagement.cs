using EcommerceApp.Domain.Entities.Identity;
using EcommerceApp.Domain.Interfaces.Authentication;
using EcommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EcommerceApp.Infrastructure.Reposatories.Authentication;

public class TokenManagement(AppDbContext context, IConfiguration config) : ITokenManagement
{
    public async Task<int> AddRefreshToken(string userId, string refreshToken)
    {
        await context.RefreshTokens.AddAsync(new RefreshToken
        {
            UserId = userId,
            Token = refreshToken
        });
        return await context.SaveChangesAsync();
    }

    public string GenerateToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!));
        var crd = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddHours(2);
        var token = new JwtSecurityToken(
            issuer: config["JWT:Issuer"],
            audience: config["JWT:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: crd);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GetRefreshToken()
    {
        const int byteSize = 64;
        var randomBytes = new byte[byteSize];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        return  Convert.ToBase64String(randomBytes);
    }

    public List<Claim> GetUserClaimsFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        if (jwtToken is not null)
            return jwtToken.Claims.ToList();
        else
            return [];
    }

    public async Task<string> GetUserIdByRefreshToken(string refreshToken)
        => (await context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken))!.UserId;
    public async Task<int> UpdateRefreshToken(string userId, string refreshToken)
    {
        var user = await context.RefreshTokens.FirstOrDefaultAsync(_ => _.Token == refreshToken);
        if (user is null) return -1;
        user.Token = refreshToken;
        return await context.SaveChangesAsync();
    }

    public async Task<bool> ValidateRefreshToken(string refreshToken)     
    {
        var user = await context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);

        if (user is null)
            return false;

        return true;
    }
}
