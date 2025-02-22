using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Application.Security.Encryption;
using Application.Security.Extensions;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Application.Security.JWT;

public class JwtHelper : ITokenHelper
{
    private readonly TokenOptions _tokenOptions;

    public JwtHelper(TokenOptions tokenOptions)
    {
        _tokenOptions = tokenOptions;
    }

    public virtual AccessToken CreateToken(User user, IList<OperationClaim> operationClaims)
    {
        DateTime expirationDate = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey));
        JwtSecurityToken token = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims, expirationDate);
        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new AccessToken
        {
            Token = tokenString,
            ExpirationDate = expirationDate
        };
    }

    public RefreshToken CreateRefreshToken(User user, string ipAddress)
    {
        return new RefreshToken
        {
            UserId = user.Id,
            Token = GenerateRandomRefreshToken(),
            ExpirationDate = DateTime.UtcNow.AddDays(_tokenOptions.RefreshTokenTTL),
            CreatedByIp = ipAddress
        };
    }

    public virtual JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials, IList<OperationClaim> operationClaims, DateTime accessTokenExpiration)
    {
        return new JwtSecurityToken(
            tokenOptions.Issuer,
            tokenOptions.Audience,
            expires: accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: SetClaims(user, operationClaims),
            signingCredentials: signingCredentials
        );
    }

    protected virtual IEnumerable<Claim> SetClaims(User user, IList<OperationClaim> operationClaims)
    {
        List<Claim> claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim("firstName", user.FirstName),
        new Claim("lastName", user.LastName)
    };

        foreach (var role in operationClaims.Select(c => c.Name))
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims.ToImmutableList();
    }


    private string GenerateRandomRefreshToken()
    {
        byte[] randomBytes = new byte[32];
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}

