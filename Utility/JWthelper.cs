using EmployeeDetailsWithTab.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace EmployeeDetailsWithTab.Utility
{
    public class JWthelper
    {
        private static string SecretKey = "mysupersecret_secretkey!123dfsddfbdfbdvsdbdfbdfvsdfsddfb";
        public static string GenerateToken(InsertSignDataModel data, int expireMinutes = 30)
        {
            string Issuer = "https://localhost:7244/";
            string Audience = "https://localhost:7244/";
            string userdata = JsonConvert.SerializeObject(data);

            var symmetricKey = Encoding.UTF8.GetBytes(SecretKey);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, userdata)
            }),
                Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                Issuer= Issuer,
                Audience= Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(stoken);
        }

        public static ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };
            SecurityToken validatedToken;
            return tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
        }
    }
}