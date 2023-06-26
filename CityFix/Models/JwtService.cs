
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class JwtService
{
    private readonly string _secretKey;

    public JwtService(string secretKey)
    {
        _secretKey = secretKey;
    }

    public string Sign(object payload)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "https://localhost:44382", // Replace with your own issuer value
            audience: "https://localhost:44382", // Replace with your own audience value
            claims: GetClaims(payload),
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static Claim[] GetClaims(object payload)
    {
        var claims = new List<Claim>();

        foreach (var property in payload.GetType().GetProperties())
        {
            claims.Add(new Claim(property.Name, property.GetValue(payload)?.ToString()));
        }

        return claims.ToArray();
    }
}

