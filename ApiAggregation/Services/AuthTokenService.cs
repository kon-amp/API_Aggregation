using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiAggregation.Services
{
    /// <summary>
    /// Service for generating JWT authentication tokens.
    /// </summary>
    public class AuthTokenService
    {
        /// <summary>
        /// Generates a JWT token for a given username.
        /// </summary>
        /// <param name="username">The username for which the token is generated.</param>
        /// <returns>A string representing the generated JWT token.</returns>
        public string GenerateToken(string username)
        {
            Claim[]? claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            SymmetricSecurityKey? key = new(Encoding.UTF8.GetBytes("kon_amp_assessment_interview_2024"));
            SigningCredentials? creds = new(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken? token = new(
                issuer: "Issuer",
                audience: "Audience",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
