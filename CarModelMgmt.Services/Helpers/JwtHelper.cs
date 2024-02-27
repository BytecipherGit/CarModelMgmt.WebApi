using CarModelMgmt.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarModelMgmt.Services.Helpers
{
    public class JwtHelper
    {
        private readonly string _secretKey;
        private readonly int _tokenExpirationInMinutes;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtHelper(string secretKey, int tokenExpirationInMinutes, IHttpContextAccessor httpContextAccessor)
        {
            _secretKey = secretKey;
            _tokenExpirationInMinutes = tokenExpirationInMinutes;
            _httpContextAccessor = httpContextAccessor;
        }

        //public string GenerateJwtToken(User user, IEnumerable<string>? roles)
        //{
        //    try
        //    {
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var key = Encoding.ASCII.GetBytes(_secretKey);
        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(new[]
        //            {
        //                new Claim(ClaimTypes.Name, user.Email),
        //                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //                // Include roles in the token claims
        //                new Claim(ClaimTypes.Role, string.Join(",", roles))
        //            }),
        //            Expires = DateTime.UtcNow.AddYears(5),
        //            //Expires = user.RememberMe ? DateTime.UtcNow.AddDays(7) : DateTime.UtcNow.AddMinutes(_tokenExpirationInMinutes),
        //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //        };
        //        var token = tokenHandler.CreateToken(tokenDescriptor);
        //        return tokenHandler.WriteToken(token);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        //public string GenerateJwtTokenForMobile(User user, IEnumerable<string>? roles)
        //{
        //    try
        //    {
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var key = Encoding.ASCII.GetBytes(_secretKey);
        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(new[]
        //            {
        //                new Claim(ClaimTypes.Name, user.Email),
        //                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //                new Claim("MobileNo", user.MobileNo),
        //                new Claim("DeviceId", user.DeviceId),
        //                new Claim(ClaimTypes.Role, string.Join(",", roles))
        //                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //            }),

        //            Expires = DateTime.UtcNow.AddYears(1),
        //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //        };
        //        var token = tokenHandler.CreateToken(tokenDescriptor);
        //        return tokenHandler.WriteToken(token);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public int? GetUserIdFromJwtToken()
        //{
        //    try
        //    {
        //        string token = "";
        //        var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        //        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        //        {
        //            token = authorizationHeader.Substring("Bearer ".Length).Trim();
        //        }
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var key = Encoding.ASCII.GetBytes(_secretKey);

        //        var tokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ValidateIssuer = false,
        //            ValidateAudience = false
        //        };

        //        ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);

        //        Claim userIdClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        //        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        //        {
        //            return userId;
        //        }

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public string GetMobileFromJwtToken()
        //{
        //    try
        //    {
        //        string token = "";
        //        var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        //        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        //        {
        //            token = authorizationHeader.Substring("Bearer ".Length).Trim();
        //        }
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var key = Encoding.ASCII.GetBytes(_secretKey);

        //        var tokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ValidateIssuer = false,
        //            ValidateAudience = false
        //        };

        //        ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);

        //        Claim MobileNoClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "MobileNo");

        //        if (MobileNoClaim != null)
        //        {
        //            return MobileNoClaim.ToString();
        //        }

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public bool ValidateJwtToken(string token)
        //{
        //    try
        //    {
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var key = Encoding.ASCII.GetBytes(_secretKey);

        //        var tokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ValidateIssuer = false,
        //            ValidateAudience = false,
        //            ValidateLifetime = true,
        //            ClockSkew = TimeSpan.Zero
        //        };

        //        tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

        //        return true;
        //    }
        //    catch (SecurityTokenExpiredException)
        //    {
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
    }
}
